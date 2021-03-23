using System;
using System.Collections.Generic;
using UnityEngine;

public class ComparableInts : IComparable
{
    int value;
    public int CompareTo(object obj)
    {
        ComparableInts a = obj as ComparableInts;

        return value-a.value;

    }

    public ComparableInts(int _int)
    {
        value = _int;
    }
}
