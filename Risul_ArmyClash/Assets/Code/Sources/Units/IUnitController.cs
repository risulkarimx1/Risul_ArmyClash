using Unity.Mathematics;
using UnityEngine;

namespace Assets.Code.Sources.Units
{
    public interface IUnitController
    {
        void Configure(UnitModel unitModel);
        UnitSide UnitSide { get; }
        float3 Position { get; set; }
        float3 Rotation { get; set; }
        Transform Transform { get; }
        float Size { get; }
        float MovementSpeed { get; }
        void Hit();
        int GetId();
    }
}