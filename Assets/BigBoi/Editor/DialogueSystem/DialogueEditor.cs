using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

namespace BigBoi.DialogueSystem
{
    [CustomEditor(typeof(Dialogue))]
    public class DialogueEditor : Editor
    {
        private SerializedProperty pLines;



        private void OnEnable()
        {
            pLines = serializedObject.FindProperty("lines");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(pLines);
            }
            EditorGUILayout.EndVertical();


            serializedObject.ApplyModifiedProperties();
        }
    }
}