using Assets.Code.Sources.Units.Factory;
using Zenject;

namespace Assets.Code.Sources.Managers
{
    public enum UnitSide { SideA, SideB}
    public class GameSceneManager: IInitializable
    {
        private readonly UnitFactory _unitFactory;

        public GameSceneManager(UnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        public void Initialize()
        {
            for (int i = 0; i < 10; i++)
            {
                _unitFactory.Create(UnitSide.SideA);
            }
        }
    }
}
