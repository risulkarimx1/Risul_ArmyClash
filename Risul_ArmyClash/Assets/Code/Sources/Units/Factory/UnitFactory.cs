using Assets.Code.Sources.Managers;
using UniRx.Async;
using Zenject;

namespace Assets.Code.Sources.Units.Factory
{
    public class UnitFactory : PlaceholderFactory<UnitSide,UniTask<IUnitView>> { }
}