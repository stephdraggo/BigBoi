using System;
using UnityEngine;

namespace BigBoi.DialogueSystem
{
    /// <summary>
    /// Scriptable object holding all the information for one full dialogue.
    /// </summary>
    [CreateAssetMenu(menuName = "BigBoi/Dialogue System/Dialogue Set", fileName = "new dialogue")]
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
            public DialogueActionInfo[] actions;
        }
        

        [SerializeField, Tooltip("The lines in this dialogue.")]
        private DialogueLine[] lines;

        public DialogueLine[] Lines => lines;

        public DialogueLine activeLine;

        /// <summary>
        /// Get line at this index
        /// </summary>
        public DialogueLine GetTarget(int _index)
        {
            return Lines[_index];
        }

        /// <summary>
        /// Get index of this line
        /// </summary>
        public int GetIndex(DialogueLine _line)
        {
            return SearchingAlgorithms.LinearSearch(lines, _line);
        }
    }
}