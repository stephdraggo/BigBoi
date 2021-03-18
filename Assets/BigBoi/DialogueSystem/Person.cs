using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This text is in the person class above the namespace
/// </summary>
namespace BigBoi.DialogueSystem
{
    [CreateAssetMenu(menuName ="BigBoi/Dialogue System/Person",fileName ="new person")]
    /// <summary>
    /// This text is in the person class above the class name
    /// </summary>
    public class Person : ScriptableObject
    {
        [SerializeField] private Sprite sprite;
        public Sprite Photo => sprite;
    }
}