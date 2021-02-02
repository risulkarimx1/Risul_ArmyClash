using System.Collections.Generic;
using System.IO;
using System.Linq;
using Code.Sources.Constants;
using Newtonsoft.Json;
using UnityEngine;

namespace Sources.Units.UnitConfiguration
{
    [CreateAssetMenu(fileName = "Color To Shape Map Data", menuName = "ArmyClash/Unit/Color Shape Mapping", order = 1)]
    public class ColorToShapeMappingData : ScriptableObject
    {
        [SerializeField] private List<ColorToShapeMapModel> _colorToShapeMap = new List<ColorToShapeMapModel>();
        [SerializeField] private UnitConfigurationsData _unitConfigurationsData;
        public UnitConfigurationsData ConfigurationsData => _unitConfigurationsData;

        public List<ColorToShapeMapModel> ColorToShapeMap => _colorToShapeMap;

        public void GenerateMatrix()
        {
            LoadConfigFromResources();
            ColorToShapeMap.Clear();
            
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
                    ColorToShapeMap.Add(colorToShapeMap);
                }
            }
        }

        public ColorToShapeMapModel GetColorShapeMappedModel(ShapeModel shapeModel, ColorModel colorModel)
        {
            var mapModel = ColorToShapeMap
                .FirstOrDefault(c => c.ShapeType == shapeModel.ShapeType 
                                     && c.ColorType == colorModel.ColorType);
            if (mapModel == null) Debug.Log($"NO map model found");
            return mapModel;
        }

        public void LoadConfigFromResources()
        {
            _unitConfigurationsData = Resources.Load<UnitConfigurationsData>(Constants.UnitConfigurationDataPath);
        }

        public void Load()
        {
            var jsonString = File.ReadAllText(Constants.ColorMapJsonFilePath);
            var listOfMap = JsonConvert.DeserializeObject<List<ColorToShapeMapModel>>(jsonString);
            Debug.Log($"Loaded File{jsonString}");
        }

        public void Save()
        {
            var jsonString = JsonConvert.SerializeObject(_colorToShapeMap, Formatting.Indented);
            File.WriteAllText(Constants.ColorMapJsonFilePath,jsonString);
            Debug.Log($"Saved File at {Constants.ColorMapJsonFilePath}");
            Debug.Log($"{jsonString}");
        }
    }
}