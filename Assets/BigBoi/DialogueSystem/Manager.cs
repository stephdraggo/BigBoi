using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BigBoi.DialogueSystem
{
    public class Manager : MonoBehaviour
    {
        #region instance
        public static Manager instance;
        private void Awake()
        {
            if (instance == null) //if none
            {
                instance = this; //it this
            }
            else if (instance != this) //if one, but not this
            {
                Destroy(this); //get rid of this
                return;
            }
            DontDestroyOnLoad(this); //make this immortal
        }
        #endregion

        [SerializeField]
        private Sprite unknownFace;
        public Sprite UnknownFace => unknownFace;

        [Serializable]
        public struct CanvasElements
        {
            public Image faceCam;
            public Text nameText;
            public Text dialogueText;
            public GameObject panel;
            public GameObject buttonPrefab;
        }
        [SerializeField]
        private CanvasElements canvasParts;
        public CanvasElements CanvasParts => canvasParts;




        void Start()
        {

        }

        void Update()
        {

        }

        public void StartDialogue(Dialogue _dialogue)
        {
            CanvasParts.panel.SetActive(true);
            _dialogue.Lines[0].UpdateUI(_dialogue);
        }
    }
    public enum Expressions
    {
        Neutral,
        Happy,
        Sad,
        Angry,
        Embarrassed,
    }
    public enum ActionTypes
    {
        Next,
        End,
        Choice,
    }
}