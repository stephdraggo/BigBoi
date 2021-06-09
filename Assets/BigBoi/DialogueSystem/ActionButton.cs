using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace BigBoi.DialogueSystem
{
    /// <summary>
    /// Contains information for one dialogue action.
    /// Type of action, text to display on button, and target index for "JumpTo" type actions.
    /// </summary>
    [Serializable]
    public class ActionButton
    {
        /// <summary>
        /// Type of this action.
        /// Determines what line of dialogue this action will lead to, if any.
        /// </summary>
        [SerializeField, Tooltip("Type of this action. Determines what line of dialogue this will lead to, if any.")]
        private ActionTypes type;

        public ActionTypes Type => type;

        /// <summary>
        /// Text to display on this action's button.
        /// </summary>
        [SerializeField, Tooltip("Display text for this action.")]
        private string label;

        public string Label => label;

        /// <summary>
        /// Index for "JumpTo" actions to target, disregarded for other types.
        /// </summary>
        [SerializeField, Tooltip("Target index for actions of type 'JumpTo', disregarded on other types.")]
        private int target;

        public int Target(Line _line, Dialogue _dialogue)
        {
            switch (type)
            {
                case ActionTypes.Next:
                    return _dialogue.LineIndex(_line) + 1;

                case ActionTypes.End:
                    return -1;

                case ActionTypes.JumpTo:
                    return target;

                default:
                    return -1;
            }
        }

        /// <summary>
        /// Allows adding completely custom methods to this button.
        /// Easy implementation of a friendship system, or opening quest menus etc.
        /// Must be a prefab object.
        /// </summary>
        [SerializeField,
         Tooltip(
             "Drag PREFAB objects to assign extra custom methods to this method.\nFor example friendship point system or open a quest panel.")]
        private Button.ButtonClickedEvent clickedEvent = new Button.ButtonClickedEvent();

        public Button.ButtonClickedEvent ClickedEvent => clickedEvent;
    }

    #region editor class

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ActionButton))]
    public class ActionButtonEditor : PropertyDrawer
    {
        public bool IsJumpTo(SerializedProperty _property)
        {
            return ((ActionTypes) _property.enumValueIndex) == ActionTypes.JumpTo;
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

            return EditorGUI.GetPropertyHeight(pClickEvent) +
                   (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) *
                   (IsJumpTo(pType) ? 3 : 2);
        }
    }
#endif

    #endregion
}