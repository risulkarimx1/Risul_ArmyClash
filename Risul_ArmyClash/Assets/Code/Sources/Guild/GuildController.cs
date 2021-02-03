using System.Collections.Generic;
using Assets.Code.Sources.Managers;
using Assets.Code.Sources.Units.UnitConfiguration;

namespace Assets.Code.Sources.Guild
{
    public class GuildController
    {
        private List<UnitController> _unitSideA;
        private List<UnitController> _unitSideB;

        public GuildController()
        {
            _unitSideA = new List<UnitController>();
            _unitSideB = new List<UnitController>();
        }

        public void AddUnit(UnitController unitController)
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
    }
}
