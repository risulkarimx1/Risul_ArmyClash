using System;
using UnityEngine;

namespace Assets.Code.Sources.Units.UnitConfiguration
{
    [Serializable]
    public class ColorModel
    {
        [SerializeField] private ColorType _colorType;
        [SerializeField] private Color _color;

        public Color Color => _color;
        public ColorType ColorType => _colorType;

        public override string ToString()
        {
            return _colorType.ToString();
        }
    }
}