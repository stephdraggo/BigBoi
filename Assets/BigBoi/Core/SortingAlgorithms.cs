using System;
using System.Collections.Generic;

namespace BigBoi
{
    public static class SortingAlgorithms
    {
        /// <summary>
        /// Uses maths to efficiently sort objects with integer values.
        /// Works best for groups of values close in range.
        /// Requires IValue instead of IComparable since Counting sort does not do any comparisons for the sorting and instead relies on just counting numbers.
        /// IValue contains the method GetValue() which returns an int.
        /// </summary>
        /// <typeparam name="T">generic object that implements IValue</typeparam>
        /// <param name="_list">list of generic objects T</param>
        public static List<T> CountingSort<T>(this List<T> _list) where T : IValue
        {
            //temporary storage list
            List<T> newList = new List<T>();

            //set min and max values to the first integer
            int min = _list[0].GetValue();
            int max = _list[0].GetValue();

            //find actual min and max values
            for (int i = 0; i < _list.Count; i++) //go through given array
            {
                newList.Add(default);

                if (_list[i].GetValue() < min) //if this integer is smaller than min
                {
                    min = _list[i].GetValue(); //set min to this integer
                }
                else if (_list[i].GetValue() > max) //if this integer is bigger than max
                {
                    max = _list[i].GetValue(); //set max to this integer
                }
            }

            max++; //this prevents index out of bounds on the "counting" for loop

            //new empty list
            List<int> valueCount = new List<int>();

            //correct length
            for (int i = 0; i < (max - min); i++)
            {
                valueCount.Add(0);
            }

            //count how many of each value
            for (int i = 0; i < _list.Count; i++)
            {
                valueCount[_list[i].GetValue() - min]++;
            }

            //add values together
            for (int i = 1; i < valueCount.Count; i++)
            {
                valueCount[i] += valueCount[i - 1];
            }

            //assign to new list
            foreach (T item in _list)
            {
                int valueCountIndex = item.GetValue() - min;
                int newListIndex = valueCount[valueCountIndex] - 1;

                newList[newListIndex] = item;
                valueCount[valueCountIndex]--;
            }

            //give list pls
            return newList;
        }

    }
}