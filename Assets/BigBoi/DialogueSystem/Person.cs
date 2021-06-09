using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AnimatedValues;

#endif

namespace BigBoi.DialogueSystem
{
    /// <summary>
    /// Unique data for a character in dialogue.
    /// Allows name and face to be hidden and revealed based on bools.
    /// Shows the correct sprite for whichever expression the character is supposed to have.
    /// </summary>
    [CreateAssetMenu(menuName = "BigBoi/Dialogue System/Person", fileName = "new person")]
    public class Person : ScriptableObject
    {
        /// <summary>
        /// Shows name if true, else shows "???"
        /// </summary>
        [Tooltip("If this character is known to the player. Allows name to be shown, otherwise '???'.")]
        public bool known;

        /// <summary>
        /// Shows face if true, else shows "unknown face" set in Manager.cs
        /// </summary>
        [Tooltip(
            "If the player can see this character. Displays face sprite, otherwise displays 'unknown face' from the manager.")]
        public bool seen;

        //Alias here
        [SerializeField] private bool hasAlias;

        [SerializeField] private string[] alias;


        /// <summary>
        /// Returns name of character/object if "known" is true, else returns "???"
        /// Allows getting a specific alias instead
        /// </summary>
        public string Name(int _alias = -1)
        {
            if (known)
            {
                if (hasAlias && alias.Length > 0 && _alias >= 0 && _alias < alias.Length)
                {
                    return alias[_alias];
                }

                return name;
            }

            return "???";
        }

        /// <summary>
        /// Set of face display options for character.
        /// </summary>
        [SerializeField,
         Tooltip(
             "Set of default expressions. To add or remove expressions, edit the 'ExpressionSet' struct in Person.cs and the 'Expressions' enum in Manager.cs.")]
        private ExpressionSet pictures;

        /// <summary>
        /// Default expression options for the character.
        /// To add or remove expressions, edit this struct and the 'Expressions' enum in Manager.cs
        /// </summary>
        [Serializable]
        public struct ExpressionSet
        {
            public Sprite neutral;
            public Sprite happy;
            public Sprite sad;
            public Sprite angry;
            public Sprite embarrassed;
        }

        /// <summary>
        /// Gets correct face sprite based on expression given.
        /// </summary>
        /// <param name="_expression">which expression to return the face for</param>
        /// <returns>the sprite to display</returns>
        public Sprite Picture(Expressions _expression)
        {
            if (seen)
            {
                switch (_expression)
                {
                    case Expressions.Neutral:
                        return pictures.neutral;

                    case Expressions.Happy:
                        return pictures.happy;

                    case Expressions.Sad:
                        return pictures.sad;

                    case Expressions.Angry:
                        return pictures.angry;

                    case Expressions.Embarrassed:
                        return pictures.embarrassed;

                    default:
                        return pictures.neutral;
                }
            }

            return Manager.instance.UnknownFace;
        }
    }

    #region editor script

#if UNITY_EDITOR
    [CustomEditor(typeof(Person))]
    public class PersonEditor : Editor
    {
        private SerializedProperty pKnown, pSeen, pHasAlias, pAlias, pPictures;

        private AnimBool HasAlias = new AnimBool();

        private void OnEnable()
        {
            pKnown = serializedObject.FindProperty("known");
            pSeen = serializedObject.FindProperty("seen");
            pHasAlias = serializedObject.FindProperty("hasAlias");
            pAlias = serializedObject.FindProperty("alias");
            pPictures = serializedObject.FindProperty("pictures");

            HasAlias.value = pHasAlias.boolValue;
            HasAlias.valueChanged.AddListener(Repaint);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(pKnown);
            EditorGUILayout.PropertyField(pSeen);
            EditorGUILayout.PropertyField(pHasAlias);

            //reveal alias creation array if aliases enabled
            HasAlias.target = pHasAlias.boolValue;
            if (EditorGUILayout.BeginFadeGroup(HasAlias.faded))
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(pAlias);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndFadeGroup();

            EditorGUILayout.PropertyField(pPictures);

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif

    #endregion
}