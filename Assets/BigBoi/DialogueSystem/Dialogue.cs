using System;
using System.Collections.Generic;
using UnityEngine;
using BigBoi;

namespace BigBoi.DialogueSystem
{
    /// <summary>
    /// Holds an array of dialogue lines.
    /// Allows finding the index of a specific line using LinearSearch() from SearchingAlgorithms.cs
    /// </summary>
    [CreateAssetMenu(menuName = "BigBoi/Dialogue System/Dialogue", fileName = "new dialogue")]
    public class Dialogue : ScriptableObject
    {
        [SerializeField, Tooltip("Edit visibility of people. Dragging a person object into this set is not necessary for them to be part of this dialogue.")]
        private People[] people;

        [Serializable]
        public class People
        {
            public Person person;

        }

        /// <summary>
        /// Array of dialogue lines.
        /// </summary>
        [SerializeField, Tooltip("Add lines to this dialogue set here.")]
        private Line[] lines;
        public Line[] Lines => lines;

        /// <summary>
        /// Method for finding the index of a specific line in this dialogue.
        /// </summary>
        /// <param name="_line">line to locate</param>
        /// <returns>index of passed line</returns>
        public int LineIndex(Line _line) => _line.LinearSearch(lines.ToList());
    }
}