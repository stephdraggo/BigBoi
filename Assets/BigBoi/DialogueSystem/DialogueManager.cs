using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BigBoi.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        #region instance
        public static DialogueManager instance;
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

        [SerializeField] private GameObject dialogueSet;


        [SerializeField] private Image faceCam;
        [SerializeField] private Text nameText, dialogueText;
        [SerializeField] private Button nextButton, byeButton;

        [SerializeField] private Button[] extraButtons;

        [SerializeField] private Dialogue activeDialogue;

        private void UpdateDisplay()
        {
            Dialogue.DialogueLine lines = activeDialogue.Lines[activeDialogue.activeIndex];

            faceCam.sprite = lines.person.Photo;
            nameText.text = lines.person.name;
            dialogueText.text = lines.dialogueText;

            ResetButtons();

            for (int i = 0; i < lines.actions.Length; i++)
            {
                int target = 0;
                switch (lines.actions[i].action)
                {
                    case DialogueActions.Next:
                        nextButton.gameObject.SetActive(true);
                        //adding jumpto method, towards the next line of dialogue in the array
                        target = activeDialogue.activeIndex + 1;
                        nextButton.onClick.AddListener(() => JumpTo(target));
                        break;
                    case DialogueActions.Bye:
                        byeButton.gameObject.SetActive(true);
                        byeButton.onClick.AddListener(EndDialogue);
                        break;
                    case DialogueActions.JumpTo:
                        extraButtons[i].gameObject.SetActive(true);
                        target = lines.actions[i].target;
                        extraButtons[i].onClick.AddListener(() => JumpTo(target));
                        break;
                    default:
                        break;
                }
            }
        }

        private void JumpTo(int _index)
        {
            activeDialogue.activeIndex = _index;

            UpdateDisplay();
        }

        private void ResetButtons()
        {
            nextButton.onClick.RemoveAllListeners();
            nextButton.gameObject.SetActive(false);

            byeButton.onClick.RemoveAllListeners();
            byeButton.gameObject.SetActive(false);

            foreach (Button _button in extraButtons)
            {
                _button.onClick.RemoveAllListeners();
                _button.gameObject.SetActive(false);
            }
        }

        private void EndDialogue()
        {
            dialogueSet.SetActive(false);
        }

        public void StartDialogue(Dialogue _dialogue,bool _startOver=true)
        {
            activeDialogue = _dialogue;
            if (_startOver)
            {
                activeDialogue.activeIndex = 0;
            }
            UpdateDisplay();
        }

        

        private void Start()
        {
            StartDialogue(activeDialogue);
        }
    }

    public enum DialogueActions
    {
        Next,
        Bye,
        JumpTo,
    }
}