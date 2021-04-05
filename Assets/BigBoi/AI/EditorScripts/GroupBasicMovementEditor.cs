using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

namespace BigBoi.AI
{
    [CustomEditor(typeof(GroupBasicMovement))]
    public class GroupBasicMovementEditor : BasicMovementEditor
    {
        protected SerializedProperty pEntities;

        protected BasicMovement[] thisGroup;
        protected new void OnEnable()
        {
            pEntities = serializedObject.FindProperty("entities");

            thisGroup = new BasicMovement[pEntities.arraySize];

            for (int i = 0; i < thisGroup.Length; i++)
            {
                //thisGroup[i] = pEntities.expos
            }

            base.OnEnable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(pEntities);

            //speed
            #region Speed
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.LabelField("Speed Values", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(pSpeed);

                for (int i = 0; i < thisGroup.Length; i++)
                {

                }

                EditorGUILayout.PropertyField(pRandomiseSpeed);
                //random speed
                EditorGUI.indentLevel++;
                randomiseSpeed.target = pRandomiseSpeed.boolValue;
                if (EditorGUILayout.BeginFadeGroup(randomiseSpeed.faded))
                {
                    EditorGUILayout.PropertyField(pRange);
                    EditorGUILayout.PropertyField(pSpeedChange);

                    //random change interval
                    changeOnTimed.target = ((BasicMovement.SpeedChangeWhen)pSpeedChange.enumValueIndex) == BasicMovement.SpeedChangeWhen.OnTimedInterval;
                    if (EditorGUILayout.BeginFadeGroup(changeOnTimed.faded))
                    {
                        EditorGUILayout.PropertyField(pInterval);
                    }
                    EditorGUILayout.EndFadeGroup();

                }
                EditorGUILayout.EndFadeGroup();
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();
            #endregion

            serializedObject.ApplyModifiedProperties();

            //base.OnInspectorGUI();
        }

        
    }
}