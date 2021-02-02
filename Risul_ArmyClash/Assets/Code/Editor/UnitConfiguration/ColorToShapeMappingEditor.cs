using UnityEditor;
using UnityEngine;

namespace Sources.Units.UnitConfiguration
{
    [CustomEditor(typeof(ColorToShapeMappingData))]
    public class ColorToShapeMappingEditor : Editor
    {
        private ColorToShapeMappingData _target;

        private void OnEnable()
        {
            _target = target as ColorToShapeMappingData;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (_target.ConfigurationsData == null)
            {
                EditorGUILayout.HelpBox($"{nameof(_target.ConfigurationsData)} Cant be empty ", MessageType.Error);
                return;
            }
            GUILayout.Space(20);
            if (GUILayout.Button("Generate /Reset Mapping Matrix"))
            {
                _target.GenerateMatrix();
            }
        }
    }
}