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

        private List<int> orderedList;

        private void Start()
        {
            orderedList = new List<int>(); //make it exist
        }

        public void CountingSortButton()
        {
            List<MyObject> intList = new List<MyObject>();
            string firstArrayText = "";
            for (int i = 0; i < 20; i++)
            {
                MyObject newObject=new MyObject();
                newObject.value = Random.Range(10, 30);
                intList.Add(newObject);
                firstArrayText += (intList[i].GetValue().ToString() + ", ");
            }

            SortingAlgorithms.CountingSort(intList);
            

            string endArrayText = "";
            for (int i = 0; i < intList.Count; i++)
            {
                endArrayText += (intList[i].GetValue().ToString() + ", ");
            }

            consoleBox.text = "The generated array is:\n" + firstArrayText + "\n\nUsing Counting Sort Algorithm gives this new array:\n" + endArrayText;
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
            int targetNumber = Random.Range(0, orderedList.Count - 1); //get random index from array
            targetNumber = orderedList[targetNumber]; //get number at that random index

            targetIndex = SearchingAlgorithms.BinarySearch<int>(orderedList, targetNumber, new CompareInts()); //use binary search

            //set up array string for display
            string arrayText = "";
            for (int i = 0; i < orderedList.Count; i++)
            {
                arrayText += (orderedList[i].ToString() + ", ");
            }

            //display result
            consoleBox.text = string.Format("Searching for integer: {0} in this ordered array:\n{1}\n" +
                "\n{0} is at index {2} of the ordered array",
                targetNumber.ToString(), arrayText, targetIndex.ToString());
        }
    }
}