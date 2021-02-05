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
            var random = Random.Range(1, 3);
            Observable.Interval(TimeSpan.FromSeconds(random)).Subscribe(_ => { Fire(ownSide, viewComponent); })
                .AddTo(viewComponent);
        }

        private void Fire(UnitSide ownSide, UnitView view)
        {
            RaycastHit hit;
            var postion = view.Position + view.transform.forward.normalized * 2;
            Debug.DrawRay(postion, view.Transform.forward);
            if (Physics.Raycast(postion, view.Transform.forward, out hit,
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