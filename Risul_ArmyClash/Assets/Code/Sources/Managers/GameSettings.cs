using Assets.Code.Sources.Guild;
using UnityEngine;

namespace Assets.Code.Sources.Managers
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "ArmyClash/Game Settings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        [Header("Guild Size")] 
        [SerializeField] private int _guildSizeA;

        [SerializeField] private int _guildSizeB;
        [SerializeField] private GuildPositioningModel _guildPositionA;
        [SerializeField] private GuildPositioningModel _guildPositionB;

        [Space(20)] 
        [Header("Hp")] 
        [SerializeField] private float _initHp;

        [SerializeField] private float _minHp;
        [SerializeField] private float _maxHp;
        
        [Header("Attack")] 
        [SerializeField] private float _initAtk;
        [SerializeField] private float _minAtk;
        [SerializeField] private float _maxAtk;

        [Header("Min Movement Speed")] 
        [SerializeField] private float _minMovementSpeed;
        [SerializeField] private float _maxMovementSpeed;

        [Header("Min Attack Speed")] 
        [SerializeField] private float _minAtkSpeed;
        [SerializeField] private float _maxAtkSpeed;

        [Header("Weapon Settings")] 
        [SerializeField] private float _weaponDamage;
        [SerializeField] private float _weaponRange;

        public int GuildSizeA => _guildSizeA;
        public int GuildSizeB => _guildSizeB;
        public GuildPositioningModel GuildPositionA => _guildPositionA;
        public GuildPositioningModel GuildPositionB => _guildPositionB;

        public float InitHp => _initHp;
        public float MinHp => _minHp;
        public float MaxHp => _maxHp;

        public float InitAtk => _initAtk;
        public float MinAtk => _minAtk;
        public float MaxAtk => _maxAtk;

        public float MinMovementSpeed => _minMovementSpeed;
        public float MaxMovementSpeed => _maxMovementSpeed;
        public float MinAtkSpeed => _minAtkSpeed;
        public float MaxAtkSpeed => _maxAtkSpeed;

        public float WeaponDamage => _weaponDamage;

        public float WeaponRange => _weaponRange;
    }
}