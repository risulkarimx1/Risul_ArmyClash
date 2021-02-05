﻿using Assets.Code.Sources.FX;
using Assets.Code.Sources.Managers;
using Assets.Code.Sources.Signals;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Code.Sources.Units
{
    public class UnitController : IUnitController
    {
        private UnitModel _unitModel;
        private readonly UnitView _unitView;
        private readonly UnitSide _unitSide;
        private readonly UnitWeapon _unitWeapon;
        private readonly SignalBus _signalBus;
        private readonly GameSettings _gameSettings;
        private readonly HitEffect.Pool _hitEffectPool;

        public UnitSide UnitSide => _unitSide;
        public Transform Transform => _unitView.Transform;
        public float Size => _unitModel.SizeModel.SizeFactor;
        public float MovementSpeed => _unitModel.MovementSpeed;

        public UnitController(UnitModel unitModel, 
            UnitView unitView, 
            UnitSide unitSide, 
            UnitWeapon unitWeapon, 
            SignalBus signalBus,
            GameSettings gameSettings,
            HitEffect.Pool hitEffectPool)
        {
            _unitModel = unitModel;
            _unitView = unitView;
            _unitSide = unitSide;
            _unitWeapon = unitWeapon;
            _signalBus = signalBus;
            _gameSettings = gameSettings;
            _hitEffectPool = hitEffectPool;
            _unitModel.Configure();
            _unitView.Configure(_unitModel);
            _unitWeapon.Configure(unitView,unitSide, _unitModel.AttackSpeed);
        }

        public void Configure(UnitModel unitModel)
        {
            _unitModel = unitModel;
            _unitModel.Configure();
            _unitView.Configure(_unitModel);
        }

        public float3 Position
        {
            get => _unitView.Position;
            set => _unitView.Position = value;
        }

        public float3 Rotation
        {
            get => _unitView.Transform.eulerAngles;
            set => _unitView.Transform.eulerAngles = value;
        }

        public void Hit()
        {
            _unitView.Rigidbody.AddRelativeForce(HitForce, ForceMode.Impulse);
            var effect = _hitEffectPool.Spawn(Position);
            DOTween.Sequence().AppendInterval(.1f).AppendCallback(() => {
            {
                _hitEffectPool.Despawn(effect);
            } });
            _unitModel.Hp.Value-=_gameSettings.WeaponDamage;
            if (_unitModel.Hp.Value <= 0)
            {
                KillUnit();
            }
        }

        public int GetId()
        {
            return _unitView.GetID();
        }

        private void KillUnit()
        {
            _signalBus.Fire( new UnitKilledSignal()
            {
                unitController = this
            });

            _unitWeapon.Destroy();
            _unitView.SetActive(false);
        }

        private Vector3 HitForce => -(_unitView.Transform.forward * Random.Range(5, 10) )+ (Vector3.right * Random.Range(-3, 3));
    }
}