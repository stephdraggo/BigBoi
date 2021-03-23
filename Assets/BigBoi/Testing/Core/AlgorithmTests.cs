using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BigBoi
{
    public class AlgorithmTests : MonoBehaviour
    {
        [SerializeField] private Text consoleBox;
        [SerializeField] private List<GameObject> objectList;

        private List<MyObject> orderedList;

        private void Start()
        {
            orderedList = new List<MyObject>(); //make it exist
        }

        public void CountingSortButton()
        {
            List<MyObject> intList = new List<MyObject>();
            string generatedListText = "";
            for (int i = 0; i < 20; i++)
            {
                MyObject newObject = new MyObject();
                newObject.value = Random.Range(10, 30);
                intList.Add(newObject);
                generatedListText += (intList[i].GetValue().ToString() + ", ");
            }

            intList = SortingAlgorithms.CountingSort(intList);


            string sortedListText = "";
            for (int i = 0; i < intList.Count; i++)
            {
                sortedListText += (intList[i].GetValue().ToString() + ", ");
            }

            consoleBox.text = "The generated list is:\n" + generatedListText + "\n\nUsing Counting Sort Algorithm gives this new list:\n" + sortedListText;

            orderedList = intList;
        }

        //public void LinearSearchButton()
        //{
        //    if (objectList.Count <= 0) //check that there is an array to search through
        //    {
        //        consoleBox.text = "You must fill the object array in inspector before using linear search";
        //        return;
        //    }

        //    int index = Random.Range(0, objectList.Count - 1); //get random index
        //    GameObject target = objectList[index]; //get object at that position
        //    index = -1; //remove saved index

        //    index = SearchingAlgorithms.LinearSearch(objectList, target); //use linear search

        //    consoleBox.text = string.Format("Searching for object: {0} in array shown in inspector.\n\nObject {0} is at position {1} in the array."
        //        , target.name, index.ToString());
        //}

        public void BinarySearchButton()
        {
            if (orderedList.Count <= 0) //check that there is an array to search through
            {
                consoleBox.text = "You must use Counting Sort to order an array before using binary search";
                return;
            }

            int targetIndex = -1; //set target index out of bounds
            MyObject targetObject = orderedList[Random.Range(0, orderedList.Count - 1)]; //get random object from list

            targetIndex = SearchingAlgorithms.BinarySearch(orderedList, targetObject); //use binary search

            //set up array string for display
            string listText = "";
            for (int i = 0; i < orderedList.Count; i++)
            {
                listText += (orderedList[i].GetValue().ToString() + ", ");
            }

            //display result
            consoleBox.text = string.Format("Searching for integer: {0} in this ordered list:\n{1}\n" +
                "\n{0} is at index {2} of the ordered array",
                targetObject.GetValue().ToString(), listText, targetIndex.ToString());
        }
    }
}