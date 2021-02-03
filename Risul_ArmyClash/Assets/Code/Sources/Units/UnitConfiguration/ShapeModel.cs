using System;
using UnityEngine;

namespace Assets.Code.Sources.Units.UnitConfiguration
{
    [Serializable]
    public class ShapeModel
    {
        [SerializeField] private ShapeType _shapeType;
        [SerializeField] private GameObject _shapeObject;
        
        [SerializeField] protected float _hp;
        [SerializeField] protected float _atk;

        public float Hp => _hp;
        public float Atk => _atk;

        public GameObject ShapeObject => _shapeObject;
        public ShapeType ShapeType => _shapeType;
    }
}