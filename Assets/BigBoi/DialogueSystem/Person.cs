using System;
using UnityEngine;

namespace BigBoi.DialogueSystem
{
    [CreateAssetMenu(menuName = "BigBoi/Dialogue System/Person", fileName = "new person")]
    public class Person : ScriptableObject
    {
        public bool known, seen;
        public string Name
        {
            get
            {
                if (known)
                {
                    return name;
                }
                return "???";
            }
        }
        [SerializeField]
        private ExpressionSet sprites;
        [Serializable]
        public struct ExpressionSet
        {
            public Sprite neutral;
            public Sprite happy;
            public Sprite sad;
            public Sprite angry;
            public Sprite embarrassed;
        }
        public Sprite Picture(Expressions _expression)
        {
            if (seen)
            {
                switch (_expression)
                {
                    case Expressions.Neutral:
                        return sprites.neutral;

                    case Expressions.Happy:
                        return sprites.happy;

                    case Expressions.Sad:
                        return sprites.sad;

                    case Expressions.Angry:
                        return sprites.angry;

                    case Expressions.Embarrassed:
                        return sprites.embarrassed;

                    default:
                        return sprites.neutral;
                }
            }
            return Manager.instance.UnknownFace;
        }
    }
}