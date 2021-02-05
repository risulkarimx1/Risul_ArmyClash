using System;
using Assets.Code.Sources.GameStateMachine;
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
        private IDisposable _fireHandle;
        private UnitSide _unitSide;
        private UnitView _unitView;
        private bool _isDisposed;
        
        public UnitWeapon(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Configure(UnitView viewComponent, UnitSide unitSide)
        {
            _unitView = viewComponent;
            _unitSide = unitSide;
            _signalBus.Subscribe<GameStateChangeSignal>(GameStateChanged);
        }

        private void GameStateChanged(GameStateChangeSignal gameStateChangeSignal)
        {
            if (gameStateChangeSignal.State == State.Battle)
            {
                StartFiring(_unitView, _unitSide);
            }

            if (gameStateChangeSignal.State == State.EndBattle)
            {
                Destroy();
            }
        }

        private void StartFiring(UnitView viewComponent, UnitSide ownSide)
        {
            // TODO : Fix magic numbers
            var random = Random.Range(0.5f, 1);
            _fireHandle = Observable.
                Interval(TimeSpan.FromSeconds(random))
                .Subscribe(_ =>
                {
                    Fire(ownSide, viewComponent);
                }).AddTo(viewComponent);
            
            _isDisposed = false;
        }

        private void Fire(UnitSide ownSide, UnitView view)
        {
            var staringPosition = view.Position + view.transform.forward.normalized * 2;
            Debug.DrawRay(staringPosition, view.Transform.forward);
            if (Physics.Raycast(view.Position, view.Transform.forward, out var hit,
                3, Constants.Constants.UnitLayer))
            {
                _signalBus.Fire(new UnitHitSignal()
                {
                    OwnSide = ownSide,
                    UnitId = hit.collider.gameObject.GetInstanceID()
                });
            }
        }

        public void Destroy()
        {
            if (_isDisposed == false)
            {
                _fireHandle.Dispose();
                _isDisposed = true;
            }
        }
    }
}