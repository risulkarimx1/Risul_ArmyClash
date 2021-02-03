using Code.Sources.Units;
using Zenject;

namespace Sources.Managers
{
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
                _unitFactory.Create();
            }
        }
    }
}
