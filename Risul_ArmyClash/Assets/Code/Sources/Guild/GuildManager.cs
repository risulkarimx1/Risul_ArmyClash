using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.Sources.Managers;
using Assets.Code.Sources.Signals;
using Assets.Code.Sources.Units;
using Assets.Code.Sources.Units.Factory;
using Assets.Code.Sources.Units.UnitConfiguration;
using Unity.Mathematics;
using Zenject;

namespace Assets.Code.Sources.Guild
{
    public class GuildManager : IDisposable
    {
        private readonly IUnitConfigGenerator _unitConfigGenerator;
        private readonly GameSettings _gameSettings;
        private readonly UnitFactory _unitFactory;
        private readonly GuildPositionController _guildPositionController;
        private readonly SignalBus _signalBus;
        private readonly List<IUnitController> _guildAList;
        private readonly List<IUnitController> _guildBList;

        public GuildManager(IUnitConfigGenerator unitConfigGenerator,
            GameSettings gameSettings,
            UnitFactory unitFactory,
            GuildPositionController guildPositionController,
            SignalBus signalBus)
        {
            _unitConfigGenerator = unitConfigGenerator;
            _gameSettings = gameSettings;
            _unitFactory = unitFactory;
            _guildPositionController = guildPositionController;
            _signalBus = signalBus;
            _guildAList = new List<IUnitController>();
            _guildBList = new List<IUnitController>();
            _signalBus.Subscribe<UnitShuffleSignal>(OnUnitShuffled);
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
                unit. Position = _guildPositionController.GetRandomPosition(unitSide);
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
        }

        public void RemoveUnit(IUnitController unitController)
        {
            unitController.KillUnit();

            if (unitController.UnitSide == UnitSide.SideA)
            {
                GuildAList.Remove(unitController);
                unitController.KillUnit();
            }
            else if (unitController.UnitSide == UnitSide.SideB)
            {
                GuildBList.Remove(unitController);
                unitController.KillUnit();
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
        
    }
}