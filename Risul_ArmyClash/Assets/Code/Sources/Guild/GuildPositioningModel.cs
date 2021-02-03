using System;
using UnityEngine;

namespace Assets.Code.Sources.Guild
{
    [Serializable]
    public struct GuildPositioningModel
    {
        [SerializeField]private Vector3 _center;
        
        [SerializeField] private float _width;
        [SerializeField] private float _length;

        public Vector3 Center => _center;
        public float Width => _width;
        public float Length => _length;
    }
}
