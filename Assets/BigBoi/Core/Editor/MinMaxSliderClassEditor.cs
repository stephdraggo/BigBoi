using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BigBoi
{
    //[CustomPropertyDrawer(typeof(MinMaxSliderProperty))]
    //public class MinMaxSliderClassEditor : PropertyDrawer
    //{ 
    //    private SerializedProperty pMin, pMax;

    //    private void OnEnable()
    //    {
    //        pMin = serializedObject.FindProperty("min");
    //        pMax = serializedObject.FindProperty("max");
    //    }

    //    public override void OnInspectorGUI()
    //    {
    //        serializedObject.Update();

    //        float min = pMin.floatValue;
    //        float max = pMax.floatValue;
    //        EditorGUILayout.MinMaxSlider("Range", ref min, ref max, 0, 1);

    //        pMin.floatValue = min;
    //        pMax.floatValue = max;

    //        serializedObject.ApplyModifiedProperties();
    //    }
    //}
}