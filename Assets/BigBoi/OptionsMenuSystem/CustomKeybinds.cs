using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BigBoi.OptionsSystem
{
    [AddComponentMenu("BigBoi/Options Menu System/Custom Keybinds")]
    public class CustomKeybinds : MonoBehaviour
    {
        [Serializable]
        public struct KeyBind
        {
            public string actionName;
            public string saveName => actionName + "KeyBind";
            public KeyCode key;
            public string keyName => key.ToString();
            public GameObject keySet;
            public Button button;
            public Text displayText, buttonText;
            public Image image;
        }

        [SerializeField, Tooltip("This prefab must follow a specific format:\n\nRoot object is a Text object - displays action name.\n\nChild object is a Button - displays key currently bound to the action.")]
        private GameObject buttonPrefab;

        [SerializeField, Tooltip("Colours to display ")]
        private Color32 baseColour, selectedColour, changedColour;

        [SerializeField, Tooltip("Implement reset button for keys?")]
        private bool implementResetButton=false;

        [SerializeField, Tooltip("Attach the button to reset the keys to their original configuration here.")]
        private Button resetButton;

        public KeyBind[] keybinds;


        private int keyCount;

        private KeyCode[] resetToTheseKeys;

        private bool waitingForInput;
        private KeyBind selectedKey;

        void Start()
        {
            keyCount = keybinds.Length;
            waitingForInput = false;
            selectedKey = keybinds[0]; //set to first keybind by default bc keybinds are not nullable, should not be accessible when not relevant

            if (!TryGetComponent(out LayoutGroup _group)) //if there is no layout group attached to this object
            {
                gameObject.AddComponent<VerticalLayoutGroup>(); //add a vertical layout group
            }

            if (resetButton != null) //if reset button attached
            {
                resetButton.onClick.AddListener(ResetKeys); //add method to the button
            }
            resetToTheseKeys = new KeyCode[keyCount]; //make array for default keys
            for (int i = 0; i < keyCount; i++) //loop through original keys (before accessing playerprefs)
            {
                resetToTheseKeys[i] = keybinds[i].key; //place default keys into array
            }


            for (int i = 0; i < keyCount; i++) //for each configurable keybind
            {
                GameObject newButton = Instantiate(buttonPrefab, transform); //generate new button

                //assign keybind struct object references
                keybinds[i].keySet = newButton;
                keybinds[i].displayText = keybinds[i].keySet.GetComponent<Text>();
                keybinds[i].button = keybinds[i].keySet.GetComponentInChildren<Button>();
                keybinds[i].buttonText = keybinds[i].button.GetComponentInChildren<Text>();
                keybinds[i].image = keybinds[i].button.GetComponent<Image>();

                keybinds[i].displayText.text = keybinds[i].actionName; //display action name



                //start() runs with no issue HOWEVER
                //when the attached method is call from the button press
                //this line is flagged as having an index out of bounds exception
                //why?
                keybinds[i].button.onClick.AddListener(
                    delegate
                    {
                        SelectKey(keybinds[i]);
                    }
                ); //add method with argument to button







                if (PlayerPrefs.HasKey(keybinds[i].saveName)) //if this key has a saved value
                {
                    keybinds[i].key = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keybinds[i].saveName)); //change bound key to match saved key string
                }
                else
                {
                    PlayerPrefs.SetString(keybinds[i].saveName, keybinds[i].keyName); //save key as string
                }

                keybinds[i].buttonText.text = keybinds[i].keyName; //update display
                newButton.name = keybinds[i].actionName + " Key Configure Button"; //name object in hierarchy

            }
        }

        private void Update()
        {
            if (waitingForInput) //if input for key bind configuring needed
            {
                Event e = Event.current; //define event
                if (e.isKey) //if event is a key press
                {
                    ChangeKey(selectedKey, e.keyCode); //call change key with selected key and e's keycode
                }
            }
        }

        void SelectKey(KeyBind _keybind)
        {
            _keybind.image.color = selectedColour; //change colour to "selected"

            selectedKey = _keybind; //assign currently selected

            waitingForInput = true; //tell update to wait for input
        }

        void ChangeKey(KeyBind _keybind, KeyCode _newCode)
        {
            _keybind.key = _newCode; //change key

            _keybind.buttonText.text = _keybind.keyName; //update display
            _keybind.image.color = changedColour; //change colour of button to "changed"

            waitingForInput = false; //tell update to stop waiting for input
        }

        void ResetKeys()
        {
            for (int i = 0; i < keyCount; i++) //for every key
            {
                ChangeKey(keybinds[i], resetToTheseKeys[i]); //call change keys for the key, feeding the default keys
            }
        }
    }
}