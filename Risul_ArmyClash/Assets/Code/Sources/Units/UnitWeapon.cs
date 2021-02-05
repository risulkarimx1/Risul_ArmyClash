using System;
using Assets.Code.Sources.GameStateMachine;
using Assets.Code.Sources.Managers;
using Assets.Code.Sources.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Code.Sources.Units
{
    public class UnitWeapon
    {
        private readonly SignalBus _signalBus;
        private readonly GameSettings _gameSettings;
        private IDisposable _fireHandle;
        private UnitSide _unitSide;
        private UnitView _unitView;
        private bool _isDisposed;
        private float _attackSpeed;

        public UnitWeapon(SignalBus signalBus, GameSettings gameSettings)
        {
            _signalBus = signalBus;
            _gameSettings = gameSettings;
        }

        public void Configure(UnitView viewComponent, UnitSide unitSide, float attackSpeed)
        {
            _unitView = viewComponent;
            _unitSide = unitSide;
            _attackSpeed = attackSpeed;
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
            _fireHandle = Observable.Interval(TimeSpan.FromSeconds(_attackSpeed))
                .Subscribe(_ =>
                {
                    Fire(ownSide, viewComponent);
                }).AddTo(viewComponent);

            _isDisposed = false;
        }

        private void Fire(UnitSide ownSide, UnitView view)
        {
            if (Physics.Raycast(view.Position, view.Transform.forward, out var hit, _gameSettings.WeaponRange, Constants.Constants.UnitLayer))
            {
                _signalBus.Fire(new UnitHitSignal
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