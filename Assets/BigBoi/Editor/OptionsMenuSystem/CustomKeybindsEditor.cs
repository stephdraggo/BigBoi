using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.IMGUI.Controls;
using BigBoi.OptionsSystem;

namespace BigBoi.InspectorEditor
{
    [CustomEditor(typeof(CustomKeybinds))]
    public class CustomKeybindsEditor : Editor
    {
        private CustomKeybinds customKeybinds;

        private SerializedProperty pButtonPrefab, pBaseColour, pSelectedColour, pChangedColour, pImplementResetButton, pResetButton, pKeybinds;

        private AnimBool resetButtonImplemented = new AnimBool();

        private void OnEnable()
        {
            customKeybinds = target as CustomKeybinds; //connect to target script

            //attach variables
            pButtonPrefab = serializedObject.FindProperty("buttonPrefab");
            pBaseColour = serializedObject.FindProperty("baseColour");
            pSelectedColour = serializedObject.FindProperty("selectedColour");
            pChangedColour = serializedObject.FindProperty("changedColour");
            pImplementResetButton = serializedObject.FindProperty("implementResetButton");
            pResetButton = serializedObject.FindProperty("resetButton");
            pKeybinds = serializedObject.FindProperty("keybinds");

            resetButtonImplemented.value = pImplementResetButton.boolValue; //align bool values
            resetButtonImplemented.valueChanged.AddListener(Repaint); //add repaint method to this bool
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //display box and instructions for button prefab
            EditorGUILayout.BeginVertical(GUI.skin.box); 
            {
                //instructions
                EditorGUILayout.LabelField("The button prefab must follow a specific format:", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("Root object is a Text object - will display action name", EditorStyles.label);
                EditorGUILayout.LabelField("Child object is a Button - will display key currently bound", EditorStyles.label);
                EditorGUI.indentLevel--;

                EditorGUILayout.PropertyField(pButtonPrefab); //see if vertical layout needed?
            }
            EditorGUILayout.EndVertical();

            //display box for choosing colours
            EditorGUILayout.BeginVertical(GUI.skin.box); 
            {
                EditorGUILayout.LabelField("Colours to Show Modified Keybinds", EditorStyles.boldLabel);

                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(pBaseColour);
                EditorGUILayout.PropertyField(pSelectedColour);
                EditorGUILayout.PropertyField(pChangedColour);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();




        }

        private void OnSceneGUI()
        {

        }
    }
}