using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigBoi
{
    public static class Conversions
    {
        /// <summary>
        /// Simple method for converting an array into a list.
        /// </summary>
        public static List<T> ArrayToList<T>(T[] _array)
        {
            List<T> newList = new List<T>();

            for (int i = 0; i < _array.Length; i++)
            {
                newList.Add(_array[i]);
            }
            
            return newList;
        }
    }
}