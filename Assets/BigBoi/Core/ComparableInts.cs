using System;
using System.Collections.Generic;
using UnityEngine;

public class ComparableInts : IComparable
{
    public int CompareTo(object obj)
    {
        ComparableInts inty = obj as ComparableInts;

        return 1;



    }


}
