using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

namespace BigBoi.DialogueSystem
{
    [CustomPropertyDrawer(typeof(ActionButton))]
    public class ActionButtonEditor : PropertyDrawer
    {
        public bool IsJumpTo(SerializedProperty _property)
        {
            return ((ActionTypes)_property.enumValueIndex) == ActionTypes.JumpTo;
        }

        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            SerializedProperty pType = _property.FindPropertyRelative("type");
            SerializedProperty pLabel = _property.FindPropertyRelative("label");
            SerializedProperty pTarget = _property.FindPropertyRelative("target");
            SerializedProperty pClickEvent = _property.FindPropertyRelative("clickedEvent");

            EditorGUI.BeginProperty(_position, _label, _property);
            {
                Rect pos = _position;
                pos.height = EditorGUIUtility.singleLineHeight;

                RenderProperty(pType, ref pos);
                RenderProperty(pLabel, ref pos);

                if (IsJumpTo(pType))
                {
                    RenderProperty(pTarget, ref pos);
                }

                pos.height = EditorGUI.GetPropertyHeight(pClickEvent);
                RenderProperty(pClickEvent, ref pos);
            }
            EditorGUI.EndProperty();
        }

        private void RenderProperty(SerializedProperty _property, ref Rect _pos)
        {
            EditorGUI.PropertyField(_pos, _property);
            
            _pos.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label)
        {
            SerializedProperty pType = _property.FindPropertyRelative("type");
            SerializedProperty pClickEvent = _property.FindPropertyRelative("clickedEvent");

            return EditorGUI.GetPropertyHeight(pClickEvent) +(EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * (IsJumpTo(pType) ? 3 : 2);
        }

        //private ActionButton actionButton;

        //private SerializedProperty pType, pLabel, pTarget;

        //private AnimBool responseIsChoice = new AnimBool();

        //private void OnEnable()
        //{
        //    actionButton = target as ActionButton;

        //    pType = serializedObject.FindProperty("type");
        //    pLabel = serializedObject.FindProperty("label");
        //    pTarget = serializedObject.FindProperty("target");

        //    responseIsChoice.value = (actionButton.Type == ActionTypes.JumpTo); //align bool to specific response type
        //    responseIsChoice.valueChanged.AddListener(Repaint); //add repaint method to this bool
        //}

        //public override void OnInspectorGUI()
        //{
        //    serializedObject.Update();

        //    EditorGUILayout.BeginVertical(GUI.skin.box); : 
        //    {
        //        EditorGUILayout.LabelField("Details for response buttons.");
        //        EditorGUI.indentLevel++;
        //        EditorGUILayout.PropertyField(pType);
        //        EditorGUILayout.PropertyField(pLabel);

        //        responseIsChoice.target = (actionButton.Type == ActionTypes.JumpTo);
        //        EditorGUILayout.BeginFadeGroup(responseIsChoice.faded);
        //        {
        //            EditorGUILayout.PropertyField(pTarget);
        //        }
        //        EditorGUILayout.EndFadeGroup();

        //        EditorGUI.indentLevel--;
        //    }
        //    EditorGUILayout.EndVertical();

        //    serializedObject.ApplyModifiedProperties();
        //}
    }
}