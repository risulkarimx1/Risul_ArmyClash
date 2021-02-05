using System;
using System.Collections.Generic;
using Assets.Code.Sources.Signals;
using Zenject;

namespace Assets.Code.Sources.Units
{
    public class UnitHitHandler : IDisposable
    {
        private readonly SignalBus _signalBus;
        private Dictionary<int, IUnitController> _unitViewToControllerMap;

        public UnitHitHandler(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _unitViewToControllerMap = new Dictionary<int, IUnitController>();
            _signalBus.Subscribe<UnitHitSignal>(OnUnitHit);
        }

        private void OnUnitHit(UnitHitSignal unitHitSignal)
        {
            var unitController = _unitViewToControllerMap[unitHitSignal.UnitId];
            if (unitController.UnitSide != unitHitSignal.OwnSide)
            {
                unitController.Hit();
            }
        }

        public void AddToMap(IUnitController unitController)
        {
            _unitViewToControllerMap.Add(unitController.GetId(), unitController);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<UnitHitSignal>(OnUnitHit);
        }
    }
}