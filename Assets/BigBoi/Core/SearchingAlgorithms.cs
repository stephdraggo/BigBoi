using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi
{
    public class SearchingAlgorithms : MonoBehaviour
    {
        /// <summary>
        /// Find the index of an object in a given array
        /// </summary>
        /// <returns>index of target in array</returns>
        public static int LinearSearch(object[] _array, object _target)
        {
            for (int i = 0; i < _array.Length; i++) //go through array
            {
                if (_array[i] == _target) //check each item if the target item
                {
                    return i; //end method and return index
                }
            }

            //if target object does not exist in this array
            Debug.LogError("Target not found in the passed array. Returning 0.");
            return 0;
        }

        /// <summary>
        /// Find the index of an int in an array of ints
        /// Very fast BUT passed array must be sorted
        /// </summary>
        /// <param name="_array">sorted array of ints</param>
        /// <param name="_target">target int</param>
        /// <returns>index of target int</returns>
        public static int BinarySearch(int[] _array, int _target)
        {
            //declare variables
            int lowIndex = 0;
            int highIndex = _array.Length;
            int midPoint;

            while (lowIndex <= highIndex) //while within bounds of high and low indices
            {
                midPoint = (lowIndex + highIndex) / 2; //set/reset midpoint
                if (_array[midPoint] > _target) //if midpoint higher than target
                {
                    highIndex = midPoint - 1; //decrease high index
                }
                else if (_array[midPoint] < _target) //if midpoint lower than target
                {
                    lowIndex = midPoint + 1; //increase low index
                }
                else //if midpoint and target are the same
                {
                    return midPoint; //target found
                }
            }

            //if target object does not exist in this array
            Debug.LogError("Target not found in the passed array. Returning 0.");
            return 0;
        }
    }
}