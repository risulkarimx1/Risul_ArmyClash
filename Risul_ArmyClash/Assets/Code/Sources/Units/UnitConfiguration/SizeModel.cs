using System;
using UnityEngine;

namespace Sources.Units.UnitConfiguration
{
    [Serializable]
    public class SizeModel: CofigurationModelBase
    {
        [SerializeField] private SizeType _sizeType;
        [Range(1,2)]
        [SerializeField] private float _sizeFactor;
    }
}