using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Modular Dialogue System in BigBoi.
/// Allows creating fully customisable speakers and dialogues.
/// Non-linear and multiple choice dialogue supported.
/// </summary>
namespace BigBoi.DialogueSystem
{
    /// <summary>
    /// Manager class for all dialogue in a scene.
    /// Must be created in each scene where dialogue will exist as it requires references to scene-specific canvas elements.
    /// </summary>
    [AddComponentMenu("BigBoi/Dialogue System/Manager")]
    public class Manager : MonoBehaviour
    {
        #region instance
        /// <summary>
        /// Static instance of the manager class for this scene.
        /// Only one instance can exist per scene, excess instances will be deleted as soon as they enter Awake().
        /// </summary>
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
        }
        #endregion

        /// <summary>
        /// Sprite displayed on any character the player cannot see instead of a face.
        /// </summary>
        [SerializeField, Tooltip("Image to display instead of a face when the player cannot see the person speaking.")]
        private Sprite unknownFace;
        public Sprite UnknownFace => unknownFace;

        /// <summary>
        /// Struct for storing all the canvas elements that update during dialogue.
        /// </summary>
        [Serializable]
        public struct CanvasElements
        {
            [Tooltip("Image object which displays the face.")]
            public Image faceCam;

            [Tooltip("Text object which displays character's name.")]
            public Text nameText;

            [Tooltip("Text object which displays the actual text of the dialogue.")]
            public Text dialogueText;

            [Tooltip("Empty object in the hierarchy to contain the buttons as they are instantiated. Must contain a layout group.")]
            public Transform buttonHolder;

            [Tooltip("Object to be enabled and disabled when displaying dialogue.")]
            public GameObject panel;

            [Tooltip("Prefab for the button. Must follow default unity hierarchy. Does not support TextMeshPro.")]
            public GameObject buttonPrefab;
        }
        [SerializeField, Tooltip("Drag and drop the canvas elements that correspond to the listed parts.")]
        private CanvasElements canvasParts;
        public CanvasElements CanvasParts => canvasParts;

        /// <summary>
        /// The currently active dialogue.
        /// </summary>
        private Dialogue activeDialogue;

        /// <summary>
        /// List of buttons currently existing.
        /// The contents is deleted and the list cleared every time a new set of buttons is instantiated.
        /// </summary>
        private List<GameObject> buttons = new List<GameObject>();

        /// <summary>
        /// Enables the dialogue interface and sets up the first display.
        /// Allows starting from a line other than the first one for non-linear dialogue.
        /// </summary>
        /// <param name="_dialogue">dialogue passed from a script outside the Dialogue System</param>
        /// <param name="_startingIndex">index of line to start at, defaults to 0</param>
        public void StartDialogue(Dialogue _dialogue, int _startingIndex = 0)
        {
            activeDialogue = _dialogue;
            CanvasParts.panel.SetActive(true);
            _dialogue.Lines[_startingIndex].UpdateUI();
        }

        /// <summary>
        /// Adds an action button to the dialogue.
        /// Connects custom method and label to button.
        /// </summary>
        /// <param name="_actionButton">Method details and label to display on this button.</param>
        /// <param name="_line">line this action button belongs to, for finding target of a "Next" type action.</param>
        public void AddButtons(ActionButton _actionButton, Line _line)
        {
            GameObject newButton = Instantiate(canvasParts.buttonPrefab, canvasParts.buttonHolder);
            newButton.GetComponent<Button>().onClick.AddListener(() => ClickButton(_actionButton.Target(_line, activeDialogue)));
            newButton.GetComponentInChildren<Text>().text = _actionButton.Label;
            buttons.Add(newButton);
        }

        /// <summary>
        /// Custom method for each action button.
        /// Updates display to target line if index >= 0.
        /// If index < 0, this cues the end of the dialogue and closes the interface.
        /// Old Buttons are deleted to make room for new ones.
        /// </summary>
        /// <param name="_target">target index for the display change</param>
        public void ClickButton(int _target)
        {
            foreach (GameObject _button in buttons)
            {
                Destroy(_button);
            }
            buttons.Clear();

            if (_target >= 0)
            {
                activeDialogue.Lines[_target].UpdateUI();
            }
            else
            {
                canvasParts.panel.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Default expression options for character faces.
    /// To add or removes options, edit this enum and the "ExpressionSet" struct in Person.cs.
    /// </summary>
    public enum Expressions
    {
        Neutral,
        Happy,
        Sad,
        Angry,
        Embarrassed,
    }
    /// <summary>
    /// Types of actions available for each line of dialogue: go to next line in order, end the dialogue, or jump to a specific line of dialogue.
    /// </summary>
    public enum ActionTypes
    {
        Next,
        End,
        JumpTo,
    }
}