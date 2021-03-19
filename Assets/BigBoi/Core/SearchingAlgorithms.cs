using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi
{
    public static class SearchingAlgorithms
    {
        /// <summary>
        /// Find the index of an object in a given array
        /// </summary>
        /// <returns>index of target in array</returns>
        public static int LinearSearch<T>(T[] _array, T _target)
        {
            for (int i = 0; i < _array.Length; i++) //go through array
            {
                if (_array[i].Equals(_target)) //check each item if the target item
                {
                    return i; //end method and return index
                }
            }

            //if target object does not exist in this array
            Debug.LogError("Target not found in the passed array. Returning 0.");
            return 0;
        }

        /// <summary>
        /// Searches through list of objects for a specific object and returns index
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="_list">list of objects</param>
        /// <param name="_target">target object</param>
        /// <param name="_comparer">compare type</param>
        /// <returns></returns>
        public static int BinarySearch<T>(List<T> _list, T _target, IComparer<T> _comparer)
        {
            //declare variables
            int lowIndex = 0;
            int highIndex = _list.Count;
            int midPoint;

            while (lowIndex <= highIndex) //while within bounds of high and low indices
            {
                midPoint = (lowIndex + highIndex) / 2; //set/reset midpoint

                int comparison = _comparer.Compare(_list[midPoint], _target); //here is the comparer, James

                if (comparison > 0) //if comparison is positive
                {
                    highIndex = midPoint - 1; //decrease high index
                }
                else if (comparison < 0) //if comparison is negative
                {
                    lowIndex = midPoint + 1; //increase low index
                }
                else //if comparison is 0
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