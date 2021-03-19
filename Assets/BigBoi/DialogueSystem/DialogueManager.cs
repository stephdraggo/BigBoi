using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Module for building dynamic dialogue trees
/// </summary>
namespace BigBoi.DialogueSystem
{
    /// <summary>
    /// Monobehaviour instance storing information for the currently active dialogue and 
    /// methods for each possible dialogue action.
    /// </summary>
    [AddComponentMenu("BigBoi/Dialogue System/Dialogue Manager Instance")]
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

        [SerializeField, Tooltip("UI object to enable and disable based on dialogue visibility.")]
        private GameObject dialogueSet;
        [SerializeField, Tooltip("Button prefab for instantiating.")]
        private GameObject buttonPrefab;
        [SerializeField, Tooltip("Button container in hierarchy.")]
        private Transform buttonParent;

        [SerializeField, Tooltip("Object to display the person's face on.")]
        private Image faceCam;
        [SerializeField, Tooltip("Display text area.")]
        private Text nameText, dialogueText;

        private List<Button> actionButtons = new List<Button>();

        private Dialogue activeDialogue;

        /// <summary>
        /// Update dialogue display with all the information for the current line along with
        /// action buttons and new methods attached to them.
        /// </summary>
        private void UpdateDisplay()
        {
            ResetButtons(); //clear previous methods from buttons

            Dialogue.DialogueLine line = activeDialogue.activeLine; //get current line of dialogue

            faceCam.sprite = line.person.Photo; //attach correct picture
            nameText.text = line.person.name; //attach correct name
            dialogueText.text = line.dialogueText; //attach correct text

            foreach (Dialogue.ActionInfo _action in line.actions) //go through actions
            {
                Button button = null; //button for label display

                button = Instantiate(buttonPrefab, buttonParent).GetComponent<Button>(); //create button
                button.gameObject.SetActive(true); //activate button

                Dialogue.DialogueLine target; //reset target for errors

                switch (_action.action)
                {
                    case DialogueActions.Next:
                        //adding jumpto method, towards the next line of dialogue in the array
                        target = activeDialogue.Lines[activeDialogue.GetIndex(activeDialogue.activeLine) + 1]; //set target
                        button.onClick.AddListener(() => JumpTo(target)); //add method with target
                        break;

                    case DialogueActions.Bye:
                        button.onClick.AddListener(EndDialogue); //add method end dialogue
                        break;

                    case DialogueActions.JumpTo:
                        target = activeDialogue.GetTarget(_action.targetIndex); //get target
                        button.onClick.AddListener(() => JumpTo(target));
                        break;

                    default:
                        break;
                }

                actionButtons.Add(button); //add to list

                button.GetComponentInChildren<Text>().text = _action.label; //label button correctly
            }
        }

        /// <summary>
        /// Change active index and update display.
        /// Maybe change from int to DialogueLine for easy dragging and dropping
        /// </summary>
        /// <param name="_target">line to jump to</param>
        private void JumpTo(Dialogue.DialogueLine _target)
        {
            activeDialogue.activeLine = _target;

            UpdateDisplay();
        }

        /// <summary>
        /// Remove old buttons.
        /// </summary>
        private void ResetButtons()
        {
            if (actionButtons.Count > 0)
            {
                foreach (Button _button in actionButtons)
                {
                    Destroy(_button.gameObject);
                }
                actionButtons.Clear();
            }
        }

        /// <summary>
        /// Deactivates object.
        /// </summary>
        private void EndDialogue()
        {
            dialogueSet.SetActive(false);
        }

        /// <summary>
        /// Sets active dialogue and determines if starting from the beginning or not.
        /// Updates Display.
        /// </summary>
        /// <param name="_dialogue">Dialogue to start</param>
        /// <param name="_startOver">whether or not to start from index 0</param>
        public void StartDialogue(Dialogue _dialogue, bool _startOver = true)
        {
            activeDialogue = _dialogue; //set active dialogue

            if (_startOver) //start at index 0?
            {
                activeDialogue.activeLine = activeDialogue.Lines[0];
            }

            UpdateDisplay();
        }
    }

    /// <summary>
    /// Types of dialogue action available.
    /// Next: move one line down;
    /// Bye: end dialogue;
    /// JumpTo: jump to a specific line of dialogue.
    /// </summary>
    public enum DialogueActions
    {
        Next,
        Bye,
        JumpTo,
    }
}