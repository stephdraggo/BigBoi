using System;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField,Tooltip("Drag PREFAB objects to assign extra custom methods to this method.\nFor example friendship point system or open a quest panel.")]
        private Button.ButtonClickedEvent clickedEvent = new Button.ButtonClickedEvent();
        public Button.ButtonClickedEvent ClickedEvent => clickedEvent;
    }
}