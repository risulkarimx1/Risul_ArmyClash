using Code.Sources.Units;

namespace Sources.Managers
{
    public class GameSceneManager
    {
        public GameSceneManager(UnitFactory unitFacotory)
        {
            for (int i = 0; i < 10; i++)
            {
                unitFacotory.Create();
            }
        }
    }
}
