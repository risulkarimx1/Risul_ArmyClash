using System.Collections.Generic;
using Assets.Code.Sources.Units;
using Assets.Code.Sources.Units.Factory;
using Assets.Code.Sources.Units.UnitConfiguration;
using UnityEngine;
using Zenject;

namespace Assets.Code.Sources.Managers
{
    public enum UnitSide { SideA, SideB}
    public class GameSceneManager: IInitializable, ITickable
    {
        private readonly UnitFactory _unitFactory;
        private readonly IUnitConfigGenerator _configGenerator;
        private List<IUnitController> unitControllers;


        public GameSceneManager(UnitFactory unitFactory, IUnitConfigGenerator configGenerator)
        {
            unitControllers = new List<IUnitController>();
            _unitFactory = unitFactory;
            _configGenerator = configGenerator;
        }

        public void Initialize()
        {
            for (int i = 0; i < 10; i++)
            {
                var unit =  _unitFactory.Create(UnitSide.SideA);
                unitControllers.Add(unit);
            }
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log($"A pressed");
                for (int i = 0; i < 10; i++)
                {
                    var model = _configGenerator.GetRandomModel();
                    unitControllers[i].Configure(model);
                }
            }
        }
    }
}
