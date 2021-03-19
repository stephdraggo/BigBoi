using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

namespace BigBoi.DialogueSystem
{
    [CustomEditor(typeof(Dialogue.ActionInfo))]
    public class DialogueActionInfoEditor : PropertyDrawer
    {
        private SerializedProperty pAction, pLabel, pTargetIndex,pJump;

        private AnimBool isJumpTo=new AnimBool();

        

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            pAction = property.FindPropertyRelative("action");
            pLabel = property.FindPropertyRelative("label");
            pTargetIndex = property.FindPropertyRelative("targetIndex");
            pJump = property.FindPropertyRelative("Jump");

            EditorGUI.BeginProperty(position, label, property);

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(pAction);
                EditorGUILayout.PropertyField(pLabel);



                isJumpTo.target = pJump.boolValue;
                if (EditorGUILayout.BeginFadeGroup(isJumpTo.faded))
                {
                    EditorGUILayout.PropertyField(pTargetIndex);
                }
                EditorGUILayout.EndFadeGroup();
            }
            EditorGUILayout.EndVertical();

            EditorGUI.EndProperty();
        }
    }
}