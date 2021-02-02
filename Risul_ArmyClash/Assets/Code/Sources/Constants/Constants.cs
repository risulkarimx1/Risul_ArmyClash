using System.IO;
using UnityEngine;

namespace Code.Sources.Constants
{
    public static class Constants
    {
        public const string ColorToShapeMapPath = "Data/UnitConfigs/ColorToShapeMap";
        public const string UnitConfigurationDataPath = "Data/UnitConfigs/UnitConfigurationData";
        public static string ColorMapJsonFilePath => Path.Combine(Application.dataPath, "ColorMap");

    }
}
