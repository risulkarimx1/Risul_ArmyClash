using Assets.Code.Sources.Guild;
using Assets.Code.Sources.Units;
using UnityEngine;
using Zenject;

namespace Assets.Code.Sources.Managers
{
    public class GameSceneManager: IInitializable, ITickable
    {
        private readonly GuildManager _guildManager;


        public GameSceneManager(GuildManager guildManager)
        {
            _guildManager = guildManager;
        }

        public void Initialize()
        {
            _guildManager.CreateGuilds();
        }

        

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _guildManager.ShuffleUnits(UnitSide.SideA);
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                _guildManager.ShuffleUnits(UnitSide.SideB);
            }
        }
    }
}
