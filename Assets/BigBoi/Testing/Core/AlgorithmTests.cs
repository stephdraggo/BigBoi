using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BigBoi
{
    public class AlgorithmTests : MonoBehaviour
    {
        [SerializeField] private Text consoleBox;
        [SerializeField] private GameObject[] objectArray;

        private int[] orderedArray;

        private void Start()
        {
            orderedArray = new int[0]; //make it exist
        }

        public void CountingSortButton()
        {
            int[] firstArray = new int[20];
            string firstArrayText = "";
            for (int i = 0; i < firstArray.Length; i++)
            {
                firstArray[i] = Random.Range(10, 30);
                firstArrayText += (firstArray[i].ToString() + ", ");
            }

            orderedArray = SortingAlgorithms.CountingSort(firstArray);
            string endArrayText = "";
            for (int i = 0; i < orderedArray.Length; i++)
            {
                endArrayText += (orderedArray[i].ToString() + ", ");
            }

            consoleBox.text = "The generated array is:\n" + firstArrayText + "\n\nUsing Counting Sort Algorithm gives this new array:\n" + endArrayText;
        }

        public void LinearSearchButton()
        {
            if (objectArray.Length <= 0) //check that there is an array to search through
            {
                consoleBox.text = "You must fill the object array in inspector before using linear search";
                return;
            }

            int index = Random.Range(0, objectArray.Length - 1); //get random index
            GameObject target = objectArray[index]; //get object at that position
            index = -1; //remove saved index

            index = SearchingAlgorithms.LinearSearch(objectArray, target); //use linear search

            consoleBox.text = string.Format("Searching for object: {0} in array shown in inspector.\n\nObject {0} is at position {1} in the array."
                , target.name, index.ToString());
        }

        public void BinarySearchButton()
        {
            if (orderedArray.Length <= 0) //check that there is an array to search through
            {
                consoleBox.text = "You must use Counting Sort to order an array before using binary search";
                return;
            }

            int targetIndex = -1; //set target index out of bounds
            int targetNumber = Random.Range(0, orderedArray.Length - 1); //get random index from array
            targetNumber = orderedArray[targetNumber]; //get number at that random index

            targetIndex = SearchingAlgorithms.BinarySearch(orderedArray, targetNumber); //use binary search

            //set up array string for display
            string arrayText = "";
            for (int i = 0; i < orderedArray.Length; i++)
            {
                arrayText += (orderedArray[i].ToString() + ", ");
            }

            //display result
            consoleBox.text = string.Format("Searching for integer: {0} in this ordered array:\n{1}\n" +
                "\n{0} is at index {2} of the ordered array",
                targetNumber.ToString(), arrayText, targetIndex.ToString());
        }
    }
}