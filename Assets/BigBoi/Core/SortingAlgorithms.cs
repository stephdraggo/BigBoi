using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi
{
    public class SortingAlgorithms : MonoBehaviour
    {
        /// <summary>
        /// Takes in an array of integers and uses counting sort to sort them in order of smallest to biggest.
        /// This algorithm is best used for integers close in value to one another
        /// </summary>
        /// <param name="_array">array of integers</param>
        /// <returns>sorted array</returns>
        public static int[] CountingSort(int[] _array)
        {
            int[] newArray = new int[_array.Length]; //new array of same length

            //set min and max values to the first integer
            int min = _array[0];
            int max = _array[0];

            //find actual min and max values
            for (int i = 0; i < _array.Length; i++) //go through given array
            {
                if (_array[i] < min) //if this integer is smaller than min
                {
                    min = _array[i]; //set min to this integer
                }
                else if (_array[i] > max) //if this integer is bigger than max
                {
                    max = _array[i]; //set max to this integer
                }
            }

            //array for recording frequency of each unique value
            int[] countOfValues = new int[max - min + 1]; //length of array is range of values + 1

            for (int i = 0; i < _array.Length; i++) //go through given array again
            {
                // _array[i]-min is the index in the frequency counting array that _array[i] belongs at
                //this is for finding duplicate values
                countOfValues[_array[i] - min]++; //add to the count of this index
            }

            countOfValues[0]--; //not sure what this line is all about

            for (int i = 1; i < countOfValues.Length; i++) //go through frequencies array
            {
                countOfValues[i] = countOfValues[i] + countOfValues[i - 1]; //add up the values that come earlier in the array
            }

            for (int i = _array.Length - 1; i >=0 ; i--) //count down from the last index of the original array
            {
                //ima be honest I don't understand what this line does
                newArray[countOfValues[_array[i] - min]--] = _array[i];
            }

            return newArray;
        }


        private void Start()
        {
            //test
            int[] firstArray = new int[10];
            string firstArrayText = "";
            for (int i = 0; i < firstArray.Length; i++)
            {
                firstArray[i] = Random.Range(10, 20);
                firstArrayText += (firstArray[i].ToString()+", ");
            }
            Debug.Log(firstArrayText);

            int[] endArray= CountingSort(firstArray);
            string endArrayText = "";
            for (int i = 0; i < endArray.Length; i++)
            {
                endArrayText += (endArray[i].ToString() + ", ");
            }
            Debug.Log(endArrayText);
        }
    }
}