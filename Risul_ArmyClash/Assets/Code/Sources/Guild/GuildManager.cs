using System.Collections.Generic;
using Assets.Code.Sources.Units;
using Assets.Code.Sources.Units.UnitConfiguration;

namespace Assets.Code.Sources.Guild
{
    public class GuildManager
    {
        private readonly IUnitConfigGenerator _unitConfigGenerator;
        private readonly List<IUnitController> _unitSideA;
        private readonly List<IUnitController> _unitSideB;

        public GuildManager(IUnitConfigGenerator unitConfigGenerator)
        {
            _unitConfigGenerator = unitConfigGenerator;
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
    }
}
