using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.Sources.GameStateMachine;
using Assets.Code.Sources.Managers;
using Assets.Code.Sources.Signals;
using Assets.Code.Sources.Units;
using Assets.Code.Sources.Units.Factory;
using Assets.Code.Sources.Units.UnitConfiguration;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Assets.Code.Sources.Guild
{
    public class GuildManager : IDisposable
    {
        private readonly IUnitConfigGenerator _unitConfigGenerator;
        private readonly GameSettings _gameSettings;
        private readonly UnitFactory _unitFactory;
        private readonly GuildPositionController _guildPositionController;
        private readonly UnitHitHandler _unitHitHandler;
        private readonly SignalBus _signalBus;
        private readonly GameState _gameState;
        private readonly List<IUnitController> _guildAList;
        private readonly List<IUnitController> _guildBList;

        public GuildManager(IUnitConfigGenerator unitConfigGenerator,
            GameSettings gameSettings,
            UnitFactory unitFactory,
            GuildPositionController guildPositionController,
            UnitHitHandler unitHitHandler,
            SignalBus signalBus,
            GameState gameState)
        {
            _unitConfigGenerator = unitConfigGenerator;
            _gameSettings = gameSettings;
            _unitFactory = unitFactory;
            _guildPositionController = guildPositionController;
            _unitHitHandler = unitHitHandler;
            _signalBus = signalBus;
            _gameState = gameState;
            _guildAList = new List<IUnitController>();
            _guildBList = new List<IUnitController>();
            _signalBus.Subscribe<UnitShuffleSignal>(OnUnitShuffled);
            _signalBus.Subscribe<UnitKilledSignal>(OnUnitKilled);
        }

        public List<IUnitController> GuildAList => _guildAList;

        public List<IUnitController> GuildBList => _guildBList;

        private void OnUnitShuffled(UnitShuffleSignal unitShuffleSignal)
        {
            switch (unitShuffleSignal.ShuffleType)
            {
                case ShuffleType.Position:
                    ShufflePositions(unitShuffleSignal.UnitSide);
                    break;
                case ShuffleType.Units:
                    ShuffleUnits(unitShuffleSignal.UnitSide);
                    break;
            }
        }

        public void CreateGuilds()
        {
            void SetPosition(UnitSide unitSide)
            {
                var unit = _unitFactory.Create(unitSide);
                unit.Position = _guildPositionController.GetRandomPosition(unitSide);
                unit.Rotation = _guildPositionController.GetRotation(unitSide);
                AddUnit(unit);
            }

            for (var i = 0; i < _gameSettings.GuildSizeA; i++)
            {
                SetPosition(UnitSide.SideA);
            }

            for (var i = 0; i < _gameSettings.GuildSizeB; i++)
            {
                SetPosition(UnitSide.SideB);
            }
        }

        private void AddUnit(IUnitController unitController)
        {
            switch (unitController.UnitSide)
            {
                case UnitSide.SideA:
                    GuildAList.Add(unitController);
                    break;
                case UnitSide.SideB:
                    GuildBList.Add(unitController);
                    break;
            }
            
            _unitHitHandler.AddToMap(unitController);
        }

        private void OnUnitKilled(UnitKilledSignal unitKilledSignal)
        {
            RemoveUnit(unitKilledSignal.unitController);
        }

        public void RemoveUnit(IUnitController unitController)
        {
            switch (unitController.UnitSide)
            {
                case UnitSide.SideA:
                    GuildAList.Remove(unitController);
                    break;
                case UnitSide.SideB:
                    GuildBList.Remove(unitController);
                    break;
            }

            _signalBus.Fire(new GuildScoreUpdatedSignal
            {
                TeamAScore = _gameSettings.GuildSizeB - GuildBList.Count,
                TeamBScore = _gameSettings.GuildSizeA - GuildAList.Count
            });
            
            if (GuildAList.Count <= 0 || GuildBList.Count <= 0)
            {
                _gameState.CurrentState = State.EndBattle;
            }
        }

        private void ShuffleUnits(UnitSide unitSide)
        {
            switch (unitSide)
            {
                case UnitSide.SideA:
                    GuildAList.ForEach(unit =>
                    {
                        var randomModel = _unitConfigGenerator.GetRandomModel();
                        unit.Configure(randomModel);
                    });
                    break;
                case UnitSide.SideB:
                    GuildBList.ForEach(unit =>
                    {
                        var randomModel = _unitConfigGenerator.GetRandomModel();
                        unit.Configure(randomModel);
                    });
                    break;
            }
        }

        private void ShufflePositions(UnitSide unitSide)
        {
            switch (unitSide)
            {
                case UnitSide.SideA:
                    GuildAList.ForEach(unit =>
                    {
                        unit.Position= _guildPositionController.GetRandomPosition(unitSide);
                    });
                    break;
                case UnitSide.SideB:
                    GuildBList.ForEach(unit =>
                    {
                        unit.Position =(_guildPositionController.GetRandomPosition(unitSide));
                    });
                    break;
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<UnitShuffleSignal>(OnUnitShuffled);
        }

        public float3[] GetGuildPositions(UnitSide unitSide)
        {
            if (unitSide == UnitSide.SideA)
                return GuildAList.Select(unit => unit.Position).ToArray();
            else
                return GuildBList.Select(unit => unit.Position).ToArray();
        }

        public Transform[] GetUnitTransforms(UnitSide unitSide)
        {
            if (unitSide == UnitSide.SideA)
                return GuildAList.Select(unit => unit.Transform).ToArray();
            else
                return GuildBList.Select(unit => unit.Transform).ToArray();
        }

        public float[] GetUnitSize(UnitSide unitSide)
        {
            if (unitSide == UnitSide.SideA)
                return GuildAList.Select(unit => unit.Size).ToArray();
            else
                return GuildBList.Select(unit => unit.Size).ToArray();
        }

        public float[] GetUnitMovementSpeed(UnitSide unitSide)
        {
            if (unitSide == UnitSide.SideA)
                return GuildAList.Select(unit => unit.MovementSpeed).ToArray();
            else
                return GuildBList.Select(unit => unit.MovementSpeed).ToArray();
        }
    }
}