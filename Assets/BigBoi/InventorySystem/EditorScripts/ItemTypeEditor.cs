using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

namespace BigBoi.InventorySystem
{
    [CustomEditor(typeof(ItemType))]
    public class ItemTypeEditor : Editor
    {
        private ItemType editingObject;

        private SerializedProperty pDisplayName, pDescription, pCategory, pItemID, pMeasurement, pUnits, pOneUnit, pMultipleUnits, pStack, pPossibleActions, pEdibleEffectType;

        private AnimBool measureInt, measureFloat,edible;


        private void OnEnable()
        {
            editingObject = serializedObject.targetObject as ItemType;

            pDisplayName = serializedObject.FindProperty("displayName");
            pDescription = serializedObject.FindProperty("description");

            pCategory = serializedObject.FindProperty("category");
            pItemID = serializedObject.FindProperty("itemID");

            pMeasurement = serializedObject.FindProperty("measurement");
            pUnits = serializedObject.FindProperty("units");
            pOneUnit = serializedObject.FindProperty("oneUnit");
            pMultipleUnits = serializedObject.FindProperty("multipleUnits");
            pStack = serializedObject.FindProperty("stack");

            pPossibleActions = serializedObject.FindProperty("possibleActions");
            pEdibleEffectType = serializedObject.FindProperty("edibleEffectType");



            #region animations
            //measurement units
            measureInt = new AnimBool();
            measureInt.value = (editingObject.Measurement== MeasureType.NumberCount);
            measureInt.valueChanged.AddListener(Repaint);
            measureFloat = new AnimBool();
            measureFloat.value = (editingObject.Measurement == MeasureType.FractionalAmount);
            measureFloat.valueChanged.AddListener(Repaint);

            //usage options
            edible = new AnimBool();
            edible.value = editingObject.PossibleActions.eat || editingObject.PossibleActions.drink;
            edible.valueChanged.AddListener(Repaint);
            #endregion
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //item details
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(pDisplayName);
                EditorGUILayout.PropertyField(pDescription);
            }
            EditorGUILayout.EndVertical();
            
            //category details
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(pCategory);
                EditorGUILayout.PropertyField(pItemID);
            }
            EditorGUILayout.EndVertical();

            //measurement details
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(pMeasurement);

                //units of measurement - int
                measureInt.target = (editingObject.Measurement == MeasureType.NumberCount);
                if (EditorGUILayout.BeginFadeGroup(measureInt.faded))
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(pOneUnit);
                    EditorGUILayout.PropertyField(pMultipleUnits);
                    EditorGUILayout.PropertyField(pStack);
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFadeGroup();

                //unit of measurement - float
                measureFloat.target = (editingObject.Measurement == MeasureType.FractionalAmount); ;
                if (EditorGUILayout.BeginFadeGroup(measureFloat.faded))
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(pUnits);
                    EditorGUILayout.PropertyField(pStack);
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFadeGroup();

            }
            EditorGUILayout.EndVertical();

            //usage details
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(pPossibleActions);


            }
            EditorGUILayout.EndVertical();


            serializedObject.ApplyModifiedProperties();
        }
    }
}