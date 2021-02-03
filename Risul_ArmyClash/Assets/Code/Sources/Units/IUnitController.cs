using UniRx.Async;

namespace Assets.Code.Sources.Units
{
    public interface IUnitController
    {
        UniTask Configure();
        void Configure(UnitModel unitModel);
    }
}