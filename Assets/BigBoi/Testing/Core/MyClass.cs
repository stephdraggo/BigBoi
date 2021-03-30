using System;
using UnityEngine;

public class MyClass : MonoBehaviour, IValue, IComparable
{
    public int value;

    public int CompareTo(object _obj)
    {
        if (_obj == null) //check if there is something to compare
        {
            throw new NullReferenceException("No object to compare.");
        }

        if (_obj is MyClass) //check if comparable type
        {
            int otherValue = (_obj as MyClass).GetValue();
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
