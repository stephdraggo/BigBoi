using UnityEngine;

namespace BigBoi.DialogueSystem
{
    /// <summary>
    /// Scriptable object for holding visual information about the person speaking. Have one per each unique speaker.
    /// </summary>
    [CreateAssetMenu(menuName = "BigBoi/Dialogue System/Person", fileName = "new person")]
    public class Person : ScriptableObject
    {
        [SerializeField, Tooltip("Select an image to be displayed as the face of this person.\n\nCurrently does not suppot changing expressions.")]
        private Sprite sprite;
        public Sprite Photo => sprite;

        //add known bool
        //add expressions
    }
}