using UnityEngine;

namespace Assets.Code.Sources.Managers
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "ArmyClash/Game Settings", order = 1)]
    public class GameSettings: ScriptableObject
    {
        [SerializeField] private int _guildSizeA;
        [SerializeField] private int _guildSizeB;

        public int GuildSizeA => _guildSizeA;
        public int GuildSizeB => _guildSizeB;
    }
}