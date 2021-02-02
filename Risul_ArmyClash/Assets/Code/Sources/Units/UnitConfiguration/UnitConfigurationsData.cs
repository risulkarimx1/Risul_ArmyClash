using UnityEngine;

namespace Sources.Units.UnitConfiguration
{
    [CreateAssetMenu(fileName = "Unit Configuration", menuName = "ArmyClash/Unit/Unit Configuration", order = 1)]
    public class UnitConfigurationsData : ScriptableObject
    {
        [SerializeField] private ShapeModel [] _shapeModels;
        [SerializeField] private SizeModel[] _sizeModels;
        [SerializeField] private ColorModel[] _colorModel;
        
        public ShapeModel [] ShapeModels => _shapeModels;
        public SizeModel[] SizeModels => _sizeModels;
        public ColorModel[] ColorModels => _colorModel;
    }
}
