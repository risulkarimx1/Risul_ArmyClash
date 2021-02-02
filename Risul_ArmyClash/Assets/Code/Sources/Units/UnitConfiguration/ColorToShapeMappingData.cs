using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.Units.UnitConfiguration
{
    [CreateAssetMenu(fileName = "Color To Shape Map Data", menuName = "ArmyClash/Unit/Color Shape Mapping", order = 1)]
    public class ColorToShapeMappingData : ScriptableObject
    {
        [SerializeField] private UnitConfigurationsData _unitConfigurationsData;
        [SerializeField] private List<ColorToShapeMapModel> _colorToShape = new List<ColorToShapeMapModel>();


        public UnitConfigurationsData ConfigurationsData => _unitConfigurationsData;

        public void GenerateMatrix()
        {
            _colorToShape.Clear();
            
            foreach (var shapeModel in _unitConfigurationsData.ShapeModels)
            {
                foreach (var colorModel in _unitConfigurationsData.ColorModels)
                {
                    var colorToShapeMap = new ColorToShapeMapModel()
                    {
                        ColorType = colorModel.ColorType,
                        ShapeType = shapeModel.ShapeType,
                        Atk = 0,
                        Hp = 0
                    };
                    _colorToShape.Add(colorToShapeMap);
                }
            }
        }

        public ColorToShapeMapModel GetColorShapeMappedModel(ShapeModel shapeModel, ColorModel colorModel)
        {
            var mapModel = _colorToShape
                .FirstOrDefault(c => c.ShapeType == shapeModel.ShapeType 
                                     && c.ColorType == colorModel.ColorType);
            return mapModel;
        }
    }
}