using System;
using UnityEngine;

namespace Assets.Code.Sources.Units.UnitConfiguration
{
    [Serializable]
    public class SizeModel
    {
        [SerializeField] private SizeType _sizeType;
        [Range(1, 2)] [SerializeField] private float _sizeFactor;

        [SerializeField] protected float _hp;

        public float Hp => _hp;
        public SizeType SizeType => _sizeType;

        public float SizeFactor => _sizeFactor;

        public override string ToString()
        {
            return $"{_sizeType}";
        }
    }
}