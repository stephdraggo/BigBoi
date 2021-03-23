using System;
using System.Collections.Generic;
using UnityEngine;

public class MyObject : MonoBehaviour,IValue,IComparable
{
    public int value;

    public int CompareTo(object obj)
    {
        if (obj == null)
        {
            throw new NullReferenceException("No object to compare.");
        }

        MyObject otherObj = obj as MyObject;

        if (otherObj != null)
        {
            if (value > otherObj.value) return 1;
            if (value < otherObj.value) return -1;
            return 0;
        }

        throw new InvalidOperationException("This object is not of type MyObject.");
    }

    public int GetValue()
    {
        return value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
