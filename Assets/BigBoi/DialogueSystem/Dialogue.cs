using System;
using UnityEngine;

namespace BigBoi.DialogueSystem
{
    /// <summary>
    /// Scriptable object holding all the information for one full dialogue.
    /// </summary>
    [CreateAssetMenu(menuName ="BigBoi/Dialogue System/Dialogue Set",fileName ="new dialogue")]
    public class Dialogue : ScriptableObject
    {
        /// <summary>
        /// Information for one line of dialogue. 
        /// Reference to the person speaking, text that is being said, 
        /// and possible actions to take in response.
        /// </summary>
        [Serializable]
        public struct DialogueLine 
        {
            [Tooltip("The person to display while showing this line of dialogue.")]
            public Person person;

            [TextArea, Tooltip("The text of what is being said.")]
            public string dialogueText;

            [Tooltip("Add at least one action or the dialogue display will be un-closable.")]
            public ActionInfo[] actions;
        }
        /// <summary>
        /// Information for each specific action allowed. 
        /// Type of action, action label, and target dialogue line if 
        /// action allows skipping to a non-chonological line of dialogue.
        /// </summary>
        [Serializable]
        public struct ActionInfo
        {
            [Tooltip("Type of action for this action button." +
                "\n\n'Jump To' type cannot be used with 'Next' type due to index out of bounds." +
                "\nTo give a choice to go to the next line OR a different line, " +
                "use two 'Jump To' actions with one of them holding the index for the next line.")]
            public DialogueActions action;

            [Tooltip("Text to display on the action button.")]
            public string label;

            [Tooltip("In 'Jump To' actions, give dialogue line index to jump to. Ignored in other types.")]
            public int target;
        }

        [SerializeField, Tooltip("The lines in this dialogue.")]
        private DialogueLine[] lines;

        public DialogueLine[] Lines => lines;

        public int activeIndex=0;
    }
}