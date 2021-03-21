using System;
using UnityEngine;

namespace BigBoi.DialogueSystem
{
    [Serializable]
    public class ActionButton
    {
        [SerializeField]
        private ActionTypes type;
        public ActionTypes Type => type;

        [SerializeField]
        private string label;
        public string Label => label;

        [SerializeField]
        private int target;
        public int Target(Line _line, Dialogue _dialogue)
        {
            switch (type)
            {
                case ActionTypes.Next:
                    return _dialogue.LineIndex(_line) + 1;

                case ActionTypes.End:
                    return 0;

                case ActionTypes.JumpTo:
                    return target;

                default:
                    return 0;
            }
        }
    }
}