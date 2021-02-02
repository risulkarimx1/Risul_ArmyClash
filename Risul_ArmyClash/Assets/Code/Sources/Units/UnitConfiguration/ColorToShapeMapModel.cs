using System;
using UnityEngine;

namespace Sources.Units.UnitConfiguration
{
    [Serializable]
    public class ColorToShapeMapModel: CofigurationModelBase
    {
        [SerializeField]
        private ColorType colorColorModel;
        [SerializeField]
        private ShapeType _shapeType;

        public ColorType ColorType { get; set; }

        public ShapeType ShapeType { get; set; }

        public override string ToString()
        {
            return $"{ShapeType} {ColorType}";
        }
    }
}