using System;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.DialogueSystem
{
    /// <summary>
    /// Information for each specific action allowed. 
    /// Type of action, action label, and target dialogue line if 
    /// action allows skipping to a non-chonological line of dialogue.
    /// </summary>
    [Serializable]
    public class DialogueActionInfo : UnityEngine.Object
    {
        [Tooltip("Type of action for this action button." +
            "\n\n'Jump To' type cannot be used with 'Next' type due to index out of bounds." +
            "\nTo give a choice to go to the next line OR a different line, " +
            "use two 'Jump To' actions with one of them holding the index for the next line.")]
        public DialogueActions action;

        [Tooltip("Text to display on the action button.")]
        public string label;

        public bool Jump
        {
            get
            {
                if (action == DialogueActions.JumpTo)
                {
                    return true;
                }
                return false;
            }
        }

        [Tooltip("In 'Jump To' actions, give dialogue line index to jump to. Ignored in other types.")]
        public int targetIndex;
    }
}