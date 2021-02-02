using UnityEditor;
using UnityEngine;

namespace Sources.Units.UnitConfiguration.Editor
{
    [CustomEditor(typeof(ColorToShapeMappingData))]
    public class ColorToShapeMappingEditor : UnityEditor.Editor
    {
        private ColorToShapeMappingData _target;

        private void OnEnable()
        {
            _target = target as ColorToShapeMappingData;
        }

        public override void OnInspectorGUI()
        {
            
            if (_target.ConfigurationsData == null)
            {
                EditorGUILayout.HelpBox($"{nameof(_target.ConfigurationsData)} Cant be empty ", MessageType.Error);
                return;
            }
            if (GUILayout.Button("Generate /Reset Mapping Matrix"))
            {
                _target.GenerateMatrix();
            }
            if (GUILayout.Button("Load"))
            {
                _target.GenerateMatrix();
            }
            GUILayout.Space(20);
            EditorGUILayout.BeginVertical();
            EditorGUIUtility.labelWidth = 50;
            EditorGUIUtility.fieldWidth = 10;
            foreach (var colorToShapeMap in _target.ColorToShapeMap)
            {

                EditorGUILayout.BeginHorizontal();
                colorToShapeMap.ColorType = (ColorType) EditorGUILayout.EnumPopup("Color: ", colorToShapeMap.ColorType);
                colorToShapeMap.ShapeType = (ShapeType) EditorGUILayout.EnumFlagsField("Shape: ", colorToShapeMap.ShapeType);
                colorToShapeMap.Atk = EditorGUILayout.FloatField("Attack:", colorToShapeMap.Atk);
                colorToShapeMap.Hp = EditorGUILayout.FloatField("Hp:", colorToShapeMap.Hp);
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(10);
            }
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Save"))
            {
                _target.GenerateMatrix();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}