using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UniRx.Async;
using UnityEngine;

namespace Assets.Code.Sources.Units.UnitConfiguration
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
            var mapModel = ColorToShapeMap.FirstOrDefault(c => c.ShapeType == shapeModel.ShapeType && c.ColorType == colorModel.ColorType);
            if (mapModel == null)
            {
                Debug.LogError($"No Mapping Found For this Combination. Generate the Matrix at {Constants.Constants.ColorToShapeMapPath}");
            }
            
            return mapModel;
        }

        public void LoadConfigFromResources()
        {
            _unitConfigurationsData = Resources.Load<UnitConfigurationsData>(Constants.Constants.UnitConfigurationDataPath);
        }

        public async UniTask LoadAsync(string path)
        {
            using (var reader = File.OpenText(path))
            {
                var jsonText = await reader.ReadToEndAsync();
                _colorToShapeMap = JsonConvert.DeserializeObject<List<ColorToShapeMapModel>>(jsonText);
            }
        }

        public void Save()
        {
            var jsonString = JsonConvert.SerializeObject(_colorToShapeMap, Formatting.Indented);
            File.WriteAllText(Constants.Constants.ColorMapJsonFilePath, jsonString);
            Debug.Log($"Saved File at {Constants.Constants.ColorMapJsonFilePath}");
            Debug.Log($"{jsonString}");
        }
    }
}