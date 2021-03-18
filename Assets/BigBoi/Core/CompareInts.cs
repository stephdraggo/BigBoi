using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CompareInts : IComparer<int>
{
    public int Compare(int x, int y)
    {
        // return >= 1 - the left object should be after the right
        // return <= -1 - the left object should be before the right
        // return 0 - the left and right objects are in the same position in the array
        return x - y;
    }
}
