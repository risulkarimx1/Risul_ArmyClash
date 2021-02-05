using System;
using Assets.Code.Sources.Signals;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Code.Sources.Units
{
    public class UnitWeapon
    {
        private readonly SignalBus _signalBus;

        public UnitWeapon(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Configure(UnitView viewComponent, UnitSide unitSide)
        {
            StartFiring(viewComponent, unitSide);
        }
        
        private void StartFiring(UnitView viewComponent, UnitSide ownSide)
        {
            // TODO : Fix magic numbers
            var random = Random.Range(1, 3);
            Observable.Interval(TimeSpan.FromSeconds(random)).Subscribe(_ => { Fire(ownSide, viewComponent); })
                .AddTo(viewComponent);
        }

        private void Fire(UnitSide ownSide, UnitView view)
        {
            var staringPosition = view.Position + view.transform.forward.normalized * 2;
            Debug.DrawRay(staringPosition, view.Transform.forward);
            if (Physics.Raycast(staringPosition, view.Transform.forward, out var hit,
                1, Constants.Constants.UnitLayer))
            {
                _signalBus.Fire(new UnitHitSignal()
                {
                    OwnSide = ownSide,
                    UnitId = hit.collider.gameObject.GetInstanceID()
                });
            }
        }
    }
}