using System;
using UnityEngine;

namespace Sources.Units.UnitConfiguration
{
    [Serializable]
    public class ShapeModel: CofigurationModelBase
    {
        [SerializeField] private ShapeType _shapeType;
        [SerializeField] private GameObject _shapeObject;

        public GameObject ShapeObject => _shapeObject;
        public ShapeType ShapeType => _shapeType;
    }
}