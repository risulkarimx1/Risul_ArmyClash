using Assets.Code.Sources.Guild;
using Assets.Code.Sources.Units;
using Assets.Code.Sources.Units.Factory;
using UnityEngine;
using Zenject;

namespace Assets.Code.Sources.Managers
{
    public class GameSceneManager: IInitializable, ITickable
    {
        private readonly UnitFactory _unitFactory;
        private readonly GuildManager _guildManager;
        private readonly GameSettings _gameSettings;


        public GameSceneManager(UnitFactory unitFactory, GuildManager guildManager, GameSettings gameSettings)
        {
            _unitFactory = unitFactory;
            _guildManager = guildManager;
            _gameSettings = gameSettings;
        }

        public void Initialize()
        {
            CreateGuilds();
        }

        private void CreateGuilds()
        {
            for (var i = 0; i < _gameSettings.GuildSizeA; i++)
            {
                var unit = _unitFactory.Create(UnitSide.SideA);
                _guildManager.AddUnit(unit);
            }

            for (var i = 0; i < _gameSettings.GuildSizeB; i++)
            {
                var unit = _unitFactory.Create(UnitSide.SideB);
                _guildManager.AddUnit(unit);
            }
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
