﻿using Assets.Code.Sources.Guild;
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
        [Header("Attack")] [SerializeField] private float _initAtk;
        [SerializeField] private float _minAtk;
        [SerializeField] private float _maxAtk;

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
    }
}