using System;
using UnityEngine;

namespace Sources.Units.UnitConfiguration
{
    [Serializable]
    public class ColorToShapeMapModel
    {
        [SerializeField] private ColorType colorColorModel;
        [SerializeField] private ShapeType _shapeType;
        [SerializeField] protected float _hp;
        [SerializeField] protected float _atk;


        public float Hp { get; set; }

        public float Atk { get; set; }

        public ColorType ColorType { get; set; }

        public ShapeType ShapeType { get; set; }

        public override string ToString()
        {
            return $"{ShapeType} {ColorType}";
        }
    }
}