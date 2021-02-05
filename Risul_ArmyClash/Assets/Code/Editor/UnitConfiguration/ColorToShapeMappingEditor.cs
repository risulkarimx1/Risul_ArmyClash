using Assets.Code.Sources.Constants;
using Assets.Code.Sources.Units.UnitConfiguration;
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
            
            if (GUILayout.Button("Clear Table"))
            {
                _target.ResetTable();
            }

            if (GUILayout.Button("Expand Table - After adding new Color / Shape"))
            {
                _target.ExpandTable();
            }

            if (GUILayout.Button("Load From Json"))
            {
                var path = Constants.ColorMapJsonFilePath;
                _= _target.LoadAsync(path);
                Debug.Log($"Loaded file from path {path}");
            }
            
            GUILayout.Space(20);
            EditorGUILayout.BeginVertical();
            EditorGUIUtility.labelWidth = 50;
            EditorGUIUtility.fieldWidth = 10;
            foreach (var colorToShapeMap in _target.ColorToShapeMap)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"Color: {colorToShapeMap.ColorType}");
                EditorGUILayout.LabelField($"Shape: {colorToShapeMap.ShapeType}");
                colorToShapeMap.Atk = EditorGUILayout.FloatField("Attack:", colorToShapeMap.Atk);
                colorToShapeMap.Hp = EditorGUILayout.FloatField("Hp:", colorToShapeMap.Hp);
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(10);
            }
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Save"))
            {
                _target.Save();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}