using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BigBoi.DialogueSystem
{
    [AddComponentMenu("BigBoi/Dialogue System/Manager")]
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
            public Transform buttonHolder;
            public GameObject panel;
            public GameObject buttonPrefab;
        }
        [SerializeField]
        private CanvasElements canvasParts;
        public CanvasElements CanvasParts => canvasParts;

        private Dialogue activeDialogue;

        private List<GameObject> buttons = new List<GameObject>();

        public void StartDialogue(Dialogue _dialogue)
        {
            activeDialogue = _dialogue;
            CanvasParts.panel.SetActive(true);
            _dialogue.Lines[0].UpdateUI();
        }

        public void AddButtons(ActionButton _actionButton,Line _line)
        {
            GameObject newButton = Instantiate(canvasParts.buttonPrefab, canvasParts.buttonHolder);
            newButton.GetComponent<Button>().onClick.AddListener(() => ClickButton(_actionButton.Target(_line, activeDialogue)));
            newButton.GetComponentInChildren<Text>().text = _actionButton.Label;
            buttons.Add(newButton);
        }

        public void ClickButton(int _target)
        {
            foreach (GameObject _button in buttons)
            {
                Destroy(_button);
            }
            buttons.Clear();

            if (_target > 0)
            {
                activeDialogue.Lines[_target].UpdateUI();
            }
            else
            {
                canvasParts.panel.SetActive(false);
            }
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
        JumpTo,
    }
}