using System;
using System.Collections.Generic;

namespace BigBoi
{
    public static class SortingAlgorithms
    {
        public static void CountingSort<T>(this List<T> _list) where T : IValue
        {
            //
            List<T> newList=new List<T>();


            //set min and max values to the first integer
            int min = _list[0].GetValue();
            int max = _list[0].GetValue();

            //find actual min and max values
            for (int i = 0; i < _list.Count; i++) //go through given array
            {
                if (_list[i].GetValue() < min) //if this integer is smaller than min
                {
                    min = _list[i].GetValue(); //set min to this integer
                }
                else if (_list[i].GetValue() > max) //if this integer is bigger than max
                {
                    max = _list[i].GetValue(); //set max to this integer
                }
            }

            //new empty list
            List<int> valueCount = new List<int>();

            //correct length
            for (int i = 0; i < (max-min); i++)
            {
                valueCount.Add(0);
            }

            int temp = 0;
            //add values as is
            for (int i = 0; i < _list.Count; i++)
            {
                valueCount[_list[i].GetValue()]++;
            }

            //add values together
            for (int i = 1; i < valueCount.Count; i++)
            {
                valueCount[i] += valueCount[i - 1];
            }

            //assign to new list
            foreach (T item in _list)
            {
                newList[valueCount[item.GetValue()]] = item;
                valueCount[item.GetValue()]--;
            }

            _list = newList;
        }


        /// <summary>
        /// Takes in an array of integers and uses counting sort to sort them in order of smallest to biggest.
        /// This algorithm is best used for integers close in value to one another
        /// </summary>
        /// <param name="_list">array of integers</param>
        /// <returns>sorted array</returns>
        public static List<int> CountingSortOld(List<int> _list)
        {
            int[] newArray = new int[_list.Count]; //new array of same length

            //set min and max values to the first integer
            int min = _list[0];
            int max = _list[0];

            //find actual min and max values
            for (int i = 0; i < _list.Count; i++) //go through given array
            {
                if (_list[i] < min) //if this integer is smaller than min
                {
                    min = _list[i]; //set min to this integer
                }
                else if (_list[i] > max) //if this integer is bigger than max
                {
                    max = _list[i]; //set max to this integer
                }
            }

            //array for recording frequency of each unique value
            int[] countOfValues = new int[max - min + 1]; //length of array is range of values + 1

            for (int i = 0; i < _list.Count; i++) //go through given array again
            {
                // _array[i]-min is the index in the frequency counting array that _array[i] belongs at
                //this is for finding duplicate values
                countOfValues[_list[i] - min]++; //add to the count of this index
            }

            countOfValues[0]--; //not sure what this line is all about

            for (int i = 1; i < countOfValues.Length; i++) //go through frequencies array
            {
                countOfValues[i] = countOfValues[i] + countOfValues[i - 1]; //add up the values that come earlier in the array
            }

            for (int i = _list.Count - 1; i >= 0; i--) //count down from the last index of the original array
            {
                //ima be honest I don't understand what this line does
                newArray[countOfValues[_list[i] - min]--] = _list[i];
            }

            List<int> returnList = new List<int>();

            for (int i = 0; i < newArray.Length; i++)
            {
                returnList.Add(newArray[i]);
            }

            return returnList;
        }
    }
}