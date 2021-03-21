using System;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.DialogueSystem
{
    [Serializable]
    public class Line
    {
        [SerializeField]
        private Person person;
        public Person Speaker=>person;

        [SerializeField]
        private Expressions expression;
        public Expressions Expression => expression;

        [SerializeField, TextArea]
        private string dialogueText;
        public string DialogueText => dialogueText;

        [SerializeField]
        private List<ActionButton> responses=new List<ActionButton>();
        public List<ActionButton> Responses => responses;

        

        public void UpdateUI()
        {
            Manager.instance.CanvasParts.faceCam.sprite = person.Picture(Expression);
            Manager.instance.CanvasParts.nameText.text = person.Name;
            Manager.instance.CanvasParts.dialogueText.text = DialogueText;

            foreach (ActionButton _response in responses)
            {
                Manager.instance.AddButtons(_response,this);
            }
        }
    }
}