using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BigBoi
{
    public class AlgorithmTests : MonoBehaviour
    {
        [SerializeField, Tooltip("Connect to text display output.")] private Text consoleBox;

        private List<MyClass> unsortedList = new List<MyClass>();
        private List<MyClass> sortedList = new List<MyClass>();
        private string unsortedListText = "";
        private string sortedListText = "";

        #region Button Methods
        public void GenerateListButton()
        {
            unsortedList.Clear();
            unsortedListText = "";

            for (int i = 0; i < 20; i++)
            {
                MyClass newObject = new MyClass();
                newObject.value = Random.Range(10, 30);
                unsortedList.Add(newObject);
                unsortedListText += (newObject.GetValue().ToString() + ", ");
            }

            consoleBox.text = "The randomly generated list is:\n" + unsortedListText;
        }

        public void CountingSortButton()
        {
            sortedListText = "";

            sortedList = unsortedList.CountingSort();

            for (int i = 0; i < unsortedList.Count; i++)
            {
                sortedListText += (sortedList[i].GetValue().ToString() + ", ");
            }

            consoleBox.text = "The randomly generated list is:\n" + unsortedListText + "\n\nUsing Counting Sort Algorithm gives this new list:\n" + sortedListText;
        }

        public void ShellSortButton()
        {
            sortedListText = "";

            sortedList = unsortedList.ShellSort();

            for (int i = 0; i < unsortedList.Count; i++)
            {
                sortedListText += (sortedList[i].GetValue().ToString() + ", ");
            }

            consoleBox.text = "The randomly generated list is:\n" + unsortedListText + "\n\nUsing Shell Sort Algorithm gives this new list:\n" + sortedListText;
        }

        public void LinearSearchButton()
        {
            MyClass target = unsortedList[Random.Range(0, unsortedList.Count - 1)]; //get object at that position

            int index = target.LinearSearch(unsortedList); //use linear search

            consoleBox.text = string.Format("Searching for object: {0} in list:\n{1}\n\nObject {0} is at position {2} in the unsorted array."
                , target.GetValue().ToString(), unsortedListText, index.ToString());
        }

        public void BinarySearchButton()
        {
            if (sortedList.Count <= 0) //check that there is an array to search through
            {
                consoleBox.text = "You must use Counting Sort to order an array before using binary search";
                return;
            }

            MyClass targetObject = sortedList[Random.Range(0, sortedList.Count - 1)]; //get random object from list

            int targetIndex = targetObject.BinarySearch(sortedList); //get index with binary search

            //display result
            consoleBox.text = string.Format("Searching for integer: {0} in this ordered list:\n{1}\n" +
                "\n{0} is at index {2} of the ordered array",
                targetObject.GetValue().ToString(), sortedListText, targetIndex.ToString());
        }
        #endregion
    }
}