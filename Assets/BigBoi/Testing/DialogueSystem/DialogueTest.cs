using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigBoi.DialogueSystem;

public class DialogueTest : MonoBehaviour
{
    public Dialogue dialogue;
    void Start()
    {
        Manager.instance.StartDialogue(dialogue);



    }


    public void Yeeee()
    {
        Debug.Log("yeah the method adding works");
    }
}
