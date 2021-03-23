using System;
using System.Collections.Generic;
using UnityEngine;

public class MyObject : MonoBehaviour,IValue
{
    public int value;

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
