using UnityEngine;

namespace BigBoi.DialogueSystem
{
    [CreateAssetMenu(menuName = "BigBoi/Dialogue System/Dialogue", fileName = "new dialogue")]
    public class Dialogue : ScriptableObject
    {[SerializeField]
        private Line[] lines;
        public Line[] Lines => lines;
        public Line ThisLine(int _index) => lines[_index];
        public int LineIndex(Line _line) => SearchingAlgorithms.LinearSearch(lines, _line);
    }
}