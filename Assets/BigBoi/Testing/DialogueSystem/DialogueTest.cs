using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigBoi.DialogueSystem;

public class DialogueTest : MonoBehaviour
{
    public Dialogue dialogue;
    void Start()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }
}