using UnityEngine;

namespace Assets.Code.Sources.Constants
{
    public static class Constants
    {
        public const int MenuSceneIndex = 0;
        public const int GameSceneIndex = 1;
        
        public const string ColorToShapeMapPath = "Data/UnitConfigs/ColorToShapeMap";
        public const string UnitConfigurationDataPath = "Data/UnitConfigs/UnitConfigurationData";
        public static string ColorMapJsonFilePath => $"{Application.dataPath}/ColorMap.json";
        public static string GameSettingsPath = "Data/Settings/Game Settings";
        public static LayerMask UnitLayer = 1 << 9;
    }
}