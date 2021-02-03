using Assets.Code.Sources.Guild;
using UnityEngine;

namespace Assets.Code.Sources.Managers
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "ArmyClash/Game Settings", order = 1)]
    public class GameSettings: ScriptableObject
    {
        [SerializeField] private int _guildSizeA;
        [SerializeField] private int _guildSizeB;
        [SerializeField] private GuildPositioningModel _guildPositionA;
        [SerializeField] private GuildPositioningModel _guildPositionB;
        
        public int GuildSizeA => _guildSizeA;
        public int GuildSizeB => _guildSizeB;
        public GuildPositioningModel GuildPositionA => _guildPositionA;
        public GuildPositioningModel GuildPositionB => _guildPositionB;
    }
}