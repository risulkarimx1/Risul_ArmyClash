using System.Collections.Generic;
using Assets.Code.Sources.Managers;
using Assets.Code.Sources.Units;
using Assets.Code.Sources.Units.Factory;
using Assets.Code.Sources.Units.UnitConfiguration;

namespace Assets.Code.Sources.Guild
{
    public class GuildManager
    {
        private readonly IUnitConfigGenerator _unitConfigGenerator;
        private readonly GameSettings _gameSettings;
        private readonly UnitFactory _unitFactory;
        private readonly GuildPositionController _guildPositionController;
        private readonly List<IUnitController> _unitSideA;
        private readonly List<IUnitController> _unitSideB;

        public GuildManager(IUnitConfigGenerator unitConfigGenerator, GameSettings gameSettings, UnitFactory unitFactory, GuildPositionController guildPositionController)
        {
            _unitConfigGenerator = unitConfigGenerator;
            _gameSettings = gameSettings;
            _unitFactory = unitFactory;
            _guildPositionController = guildPositionController;
            _unitSideA = new List<IUnitController>();
            _unitSideB = new List<IUnitController>();
        }

        public void AddUnit(IUnitController unitController)
        {
            switch (unitController.UnitSide)
            {
                case UnitSide.SideA:
                    _unitSideA.Add(unitController);
                    break;
                case UnitSide.SideB:
                    _unitSideB.Add(unitController);
                    break;
            }
        }

        public void ShuffleUnits(UnitSide unitSide)
        {
            switch (unitSide)
            {
                case UnitSide.SideA:
                    _unitSideA.ForEach(unit=>
                    {
                        var randomModel = _unitConfigGenerator.GetRandomModel();
                        unit.Configure(randomModel);
                    });
                    break;
                case UnitSide.SideB:
                    _unitSideB.ForEach(unit =>
                    {
                        var randomModel = _unitConfigGenerator.GetRandomModel();
                        unit.Configure(randomModel);
                    });
                    break;
            }
        }

        public void ShufflePositions(UnitSide unitSide)
        {
            switch (unitSide)
            {
                case UnitSide.SideA:
                    _unitSideA.ForEach(unit =>
                    {
                        unit.SetPosition(_guildPositionController.GetRandomPosition(unitSide));
                    });
                    break;
                case UnitSide.SideB:
                    _unitSideB.ForEach(unit =>
                    {
                        unit.SetPosition(_guildPositionController.GetRandomPosition(unitSide));
                    });
                    break;
            }
        }

        public void CreateGuilds()
        {
            void SetPosition(UnitSide unitSide)
            {
                var unit = _unitFactory.Create(unitSide);
                unit.SetPosition(_guildPositionController.GetRandomPosition(unitSide));
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
    }
}
