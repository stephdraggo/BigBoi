using System;
using UnityEngine;

public class MyObject : MonoBehaviour, IValue, IComparable
{
    public int value;

    public int CompareTo(object obj)
    {
        if (obj == null)
        {
            throw new NullReferenceException("No object to compare.");
        }

        if (obj is MyObject)
        {
            MyObject otherObj = obj as MyObject;
            int otherValue = otherObj.GetValue();
            if (value > otherValue) return 1;
            if (value < otherValue) return -1;
            return 0;
        }

        throw new InvalidOperationException("This object is not of type MyObject.");
    }

    public int GetValue()
    {
        return value;
    }
}
