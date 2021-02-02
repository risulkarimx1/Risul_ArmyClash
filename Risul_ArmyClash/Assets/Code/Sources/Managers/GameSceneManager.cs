using Code.Sources.Units;

namespace Sources.Managers
{
    public class GameSceneManager
    {
        public GameSceneManager(UnitFactory unitFacotory)
        {
            unitFacotory.Create();
        }
    }
}
