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
        private readonly List<IUnitController> _unitSideA;
        private readonly List<IUnitController> _unitSideB;

        public GuildManager(IUnitConfigGenerator unitConfigGenerator, GameSettings gameSettings, UnitFactory unitFactory)
        {
            _unitConfigGenerator = unitConfigGenerator;
            _gameSettings = gameSettings;
            _unitFactory = unitFactory;
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

        public void CreateGuilds()
        {
            for (var i = 0; i < _gameSettings.GuildSizeA; i++)
            {
                var unit = _unitFactory.Create(UnitSide.SideA);
                AddUnit(unit);
            }

            for (var i = 0; i < _gameSettings.GuildSizeB; i++)
            {
                var unit = _unitFactory.Create(UnitSide.SideB);
                AddUnit(unit);
            }
        }
    }
}
