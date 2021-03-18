using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This text is in the person class
/// </summary>
namespace BigBoi.DialogueSystem
{
    [CreateAssetMenu(menuName ="BigBoi/Dialogue System/Person",fileName ="new person")]
    public class Person : ScriptableObject
    {
        [SerializeField] private Sprite sprite;
        public Sprite Photo => sprite;
    }
}