using UnityEngine;
using UnityEditor;

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

            return EditorGUI.GetPropertyHeight(pClickEvent) + (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * (IsJumpTo(pType) ? 3 : 2);
        }
    }
}