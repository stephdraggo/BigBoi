using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

namespace BigBoi.DialogueSystem
{
    [CustomEditor(typeof(DialogueActionInfo))]
    public class DialogueActionInfoEditor : Editor
    {
        private SerializedProperty pAction, pLabel, pTargetIndex,pJump;

        private AnimBool isJumpTo=new AnimBool();

        public void OnEnable()
        {
            pAction = serializedObject.FindProperty("action");
            pLabel = serializedObject.FindProperty("label");
            pTargetIndex = serializedObject.FindProperty("targetIndex");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(pAction);
                EditorGUILayout.PropertyField(pLabel);



                isJumpTo.target = pAction.enumValueIndex == (int)DialogueActions.JumpTo;
                if (EditorGUILayout.BeginFadeGroup(isJumpTo.faded))
                {
                    EditorGUILayout.PropertyField(pTargetIndex);
                }
                EditorGUILayout.EndFadeGroup();
            }
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}