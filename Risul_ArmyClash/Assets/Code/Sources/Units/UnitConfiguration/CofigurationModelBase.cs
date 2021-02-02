using System;
using UnityEngine;

namespace Sources.Units.UnitConfiguration
{
    [Serializable]
    public abstract class CofigurationModelBase
    {
        [SerializeField] protected  float _hp;
        [SerializeField] protected float _atk;

        public float Hp { get; set; }
        public float Atk { get; set; }
    }
}