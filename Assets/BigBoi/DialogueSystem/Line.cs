using System;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.DialogueSystem
{
    /// <summary>
    /// Information specific to one line of dialogue.
    /// Person speaking, which expression to display for that person, the text of the dialogue, and possible actions to take in this line of dialogue.
    /// </summary>
    [Serializable]
    public class Line : IComparable
    {
        /// <summary>
        /// Reference to person speaking for access to display name and sprite.
        /// </summary>
        [SerializeField, Tooltip("Person saying this line of dialogue.")]
        private Person person;
        public Person Speaker => person;

        /// <summary>
        /// Which expression to access from person.
        /// </summary>
        [SerializeField, Tooltip("The expression to display for this line.")]
        private Expressions expression;
        public Expressions Expression => expression;

        /// <summary>
        /// Dialogue text to display.
        /// Supports <b>, <i> etc.
        /// </summary>
        [SerializeField, TextArea, Tooltip("Text to be displayed for this line. Use <b> for bold and <i> for italics.")]
        private string dialogueText;
        public string DialogueText => dialogueText;

        /// <summary>
        /// Array of actions available on this line of dialogue.
        /// </summary>
        [SerializeField, Tooltip("Add possible actions to this line.")]
        private List<ActionButton> responses = new List<ActionButton>();
        public List<ActionButton> Responses => responses;

        /// <summary>
        /// Update display to show this line of dialogue.
        /// </summary>
        public void UpdateUI()
        {
            Manager.instance.CanvasParts.faceCam.sprite = person.Picture(Expression);
            Manager.instance.CanvasParts.nameText.text = person.Name();
            Manager.instance.CanvasParts.dialogueText.text = DialogueText;

            foreach (ActionButton _response in responses)
            {
                Manager.instance.AddButtons(_response, this);
            }
        }

        public int CompareTo(object _obj)
        {
            if (_obj == this)
            {
                return 0;
            }
            return 1;
        }
    }
}