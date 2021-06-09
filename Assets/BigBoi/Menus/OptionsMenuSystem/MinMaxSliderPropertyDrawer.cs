using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace BigBoi
{
    [CustomPropertyDrawer(typeof(MinMaxSliderProperty))]
    public class MinMaxSliderPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            EditorGUI.BeginProperty(_position, _label, _property);

            var pMin = _property.FindPropertyRelative("min");
            var pMax = _property.FindPropertyRelative("max");
            var pCMin = _property.FindPropertyRelative("cMin");
            var pCMax = _property.FindPropertyRelative("cMax");

            float min = pMin.floatValue;
            float max = pMax.floatValue;
            Vector2 range = new Vector2(pCMin.floatValue, pCMax.floatValue);
            range = EditorGUI.Vector2Field(_position, "Range", range);
            EditorGUI.MinMaxSlider(new Rect(_position.x, _position.y * 2, _position.width,_position.height), $"Sub range {min.ToString("0.0")} : {max.ToString("0.0")}", ref min, ref max, range.x, range.y);

            pMin.floatValue = min;
            pMax.floatValue = max;
            pCMin.floatValue = range.x;
            pCMax.floatValue = range.y;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label) => EditorGUIUtility.singleLineHeight * 2;
    }
}