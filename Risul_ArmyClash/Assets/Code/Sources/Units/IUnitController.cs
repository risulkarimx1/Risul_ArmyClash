using UniRx.Async;
using UnityEngine;

namespace Assets.Code.Sources.Units
{
    public interface IUnitController
    {
        UniTask Configure();
        void Configure(UnitModel unitModel);
        UnitSide UnitSide { get; }
        void SetPosition(Vector3 position);
    }
}