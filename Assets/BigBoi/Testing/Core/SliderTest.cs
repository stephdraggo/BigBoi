using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigBoi;

public class SliderTest : MonoBehaviour
{
    public MinMaxField slider;

    private void OnValidate()
    {
        slider ??= new MinMaxField();
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
