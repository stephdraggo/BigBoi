using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi.DialogueSystem
{
    [CreateAssetMenu(menuName ="BigBoi/Dialogue System/Dialogue Set",fileName ="new dialogue")]
    public class Dialogue : ScriptableObject
    {
        [Serializable]
        public struct DialogueLine 
        {
            public Person person;
            [TextArea]
            public string dialogueText;
            public ActionInfo[] actions;
        }
        [Serializable]
        public struct ActionInfo
        {
            public DialogueActions action;
            public string label;

            [Tooltip("Only used for Jump To buttons.")]
            public int target;
        }

        [SerializeField] private DialogueLine[] lines;
        public DialogueLine[] Lines => lines;

        public int activeIndex=0;
    }
}