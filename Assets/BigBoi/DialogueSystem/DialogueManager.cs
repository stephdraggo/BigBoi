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

        [SerializeField, Tooltip("Object to display the person's face on.")]
        private Image faceCam;
        [SerializeField, Tooltip("Display text area.")]
        private Text nameText, dialogueText;
        [SerializeField, Tooltip("Default action button.")]
        private Button nextButton, byeButton;

        [SerializeField, Tooltip("Extra action buttons, for 'Jump To' actions or otherwise." +
            "\nRecommended to have at least two." +
            "\n\nMaybe implement extra button instantiate in future.")]
        private Button[] extraButtons;

        [SerializeField]private Dialogue activeDialogue; //serialised for testing

        /// <summary>
        /// Update dialogue display with all the information for the current line along with
        /// action buttons and new methods attached to them.
        /// </summary>
        private void UpdateDisplay()
        {
            Dialogue.DialogueLine line = activeDialogue.Lines[activeDialogue.activeIndex]; //get current line of dialogue

            faceCam.sprite = line.person.Photo; //attach correct picture
            nameText.text = line.person.name; //attach correct name
            dialogueText.text = line.dialogueText; //attach correct text

            ResetButtons(); //clear previous methods from buttons

            //change to foreach??
            //would mean no more stipulation about not using Next with Jump To
            for (int i = 0; i < line.actions.Length; i++) //go through actions available on this line of dialogue
            {
                Button button = null; //button for label display

                int target = 0; //reset target for errors
                switch (line.actions[i].action) //check what action type
                {
                    case DialogueActions.Next:
                        nextButton.gameObject.SetActive(true); //activate button
                        //adding jumpto method, towards the next line of dialogue in the array
                        target = activeDialogue.activeIndex + 1; //set target
                        nextButton.onClick.AddListener(() => JumpTo(target)); //add method with target
                        button = nextButton; //get which button to re-label
                        break;

                    case DialogueActions.Bye:
                        byeButton.gameObject.SetActive(true);
                        byeButton.onClick.AddListener(EndDialogue); //add method end dialogue
                        button = byeButton;
                        break;

                    case DialogueActions.JumpTo:
                        extraButtons[i].gameObject.SetActive(true);
                        target = line.actions[i].target;
                        extraButtons[i].onClick.AddListener(() => JumpTo(target));
                        button = extraButtons[i];
                        break;

                    default:
                        break;
                }

                button.GetComponentInChildren<Text>().text = line.actions[i].label; //label button correctly
            }
        }

        /// <summary>
        /// Change active index and update display.
        /// Maybe change from int to DialogueLine for easy dragging and dropping
        /// </summary>
        /// <param name="_index">index of line to jump to</param>
        private void JumpTo(int _index)
        {
            activeDialogue.activeIndex = _index;

            UpdateDisplay();
        }

        /// <summary>
        /// Remove methods and deactivate buttons.
        /// </summary>
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
                activeDialogue.activeIndex = 0;
            }

            UpdateDisplay();
        }



        private void Start()
        {
            StartDialogue(activeDialogue);
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