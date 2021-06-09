using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AnimatedValues;

#endif

namespace BigBoi.Menus.OptionsMenuSystem
{
    [AddComponentMenu("BigBoi/Options Menu System/Custom Keybinds (Unity default)")]
    public class UnityCustomKeybinds : MonoBehaviour
    {
        [Serializable]
        public struct KeyBind
        {
            public string actionName;
            public string saveName => actionName + "KeyBind";
            public KeyCode key;
            public string keyName => key.ToString();

            //do not want these to display on inspector
            [HideInInspector] public GameObject keySet;
            [HideInInspector] public Button keyButton;
            [HideInInspector] public Text displayText, buttonText;
            [HideInInspector] public Image keyImage;
        }

        [SerializeField,
         Tooltip(
             "This prefab must follow a specific format:\n\nRoot object is a Text object - displays action name.\n\nChild object is a Button - displays key currently bound to the action.")]
        private GameObject buttonPrefab;

        [SerializeField, Tooltip("Colours to display ")]
        private Color32 baseColour, selectedColour, changedColour;

        [SerializeField, Tooltip("Implement reset button for keys?")]
        private bool includeResetButton = false;

        [SerializeField, Tooltip("Attach the button to reset the keys to their original configuration here.")]
        private Button resetButton;

        public KeyBind[] keybinds;


        private int keyCount;

        private KeyCode[] resetToTheseKeys;

        private bool waitingForInput;
        private KeyBind selectedKey;


        private void OnValidate()
        {
            if (TryGetComponent(out LayoutGroup _group)) //if there is a layout group attached to this object
            {
                if (!_group.isActiveAndEnabled) //if not enabled
                {
                    _group.enabled = true; //enable it
                }
            }
            else gameObject.AddComponent<VerticalLayoutGroup>(); //add a vertical layout group
        }

        private void Start()
        {
            keyCount = keybinds.Length;
            waitingForInput = false;
            selectedKey =
                keybinds
                    [0]; //set to first keybind by default bc keybinds are not nullable, should not be accessible when not relevant

            if (resetButton != null) //if reset button attached
            {
                resetButton.onClick.AddListener(ResetKeys); //add method to the button
                resetButton.GetComponentInChildren<Text>().text = "Reset Keybinds";
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
                keybinds[i].keyButton = keybinds[i].keySet.GetComponentInChildren<Button>();
                keybinds[i].buttonText = keybinds[i].keyButton.GetComponentInChildren<Text>();
                keybinds[i].keyImage = keybinds[i].keyButton.GetComponent<Image>();

                keybinds[i].displayText.text = keybinds[i].actionName; //display action name
                keybinds[i].keyImage.color = baseColour; //make sure button is base colour

                KeyBind newKeyBind = keybinds[i]; //separate parameter (this fixes index out of bounds)
                keybinds[i].keyButton.onClick
                    .AddListener(() => SelectKey(newKeyBind)); //add method with parameter to button


                if (PlayerPrefs.HasKey(keybinds[i].saveName)) //if this key has a saved value
                {
                    keybinds[i].key =
                        (KeyCode) Enum.Parse(typeof(KeyCode),
                            PlayerPrefs.GetString(keybinds[i].saveName)); //change bound key to match saved key string
                }
                else
                {
                    PlayerPrefs.SetString(keybinds[i].saveName, keybinds[i].keyName); //save key as string
                }

                keybinds[i].buttonText.text = keybinds[i].keyName; //update display
                newButton.name = keybinds[i].actionName + " Key Configure Button"; //name object in hierarchy
            }
        }


        private void OnGUI()
        {
            if (waitingForInput) //if input for key bind configuring needed
            {
                Event e = Event.current; //define event
                if (e != null) //if e is not null
                {
                    if (Input.GetMouseButton(0))
                    {
                        selectedKey.keyImage.color = baseColour;
                        waitingForInput = false;
                        return;
                    }

                    if (e.isKey) //if event is a key press
                    {
                        ChangeKey(selectedKey, e.keyCode); //call change key with selected key and e's keycode
                    }
                }
            }
        }


        //apparently this method gets index out of bounds
        void SelectKey(KeyBind _keybind)
        {
            _keybind.keyImage.color = selectedColour; //change colour to "selected"

            selectedKey = _keybind; //assign currently selected

            waitingForInput = true; //tell update to wait for input
        }

        void ChangeKey(KeyBind _keybind, KeyCode _newCode)
        {
            _keybind.key = _newCode; //change key

            _keybind.buttonText.text = _keybind.keyName; //update display
            _keybind.keyImage.color = changedColour; //change colour of button to "changed"

            PlayerPrefs.SetString(_keybind.saveName, _keybind.keyName);

            waitingForInput = false; //tell update to stop waiting for input
        }

        void ResetKeys()
        {
            for (int i = 0; i < keyCount; i++) //for every key
            {
                ChangeKey(keybinds[i], resetToTheseKeys[i]); //call change keys for the key, feeding the default keys

                keybinds[i].keyImage.color = baseColour; //reset colour of button as well
            }
        }
    }

    #region editor script

#if UNITY_EDITOR
    [CustomEditor(typeof(UnityCustomKeybinds))]
    [CanEditMultipleObjects]
    public class CustomKeybindsEditor : Editor
    {
        private SerializedProperty pButtonPrefab,
            pBaseColour,
            pSelectedColour,
            pChangedColour,
            pIncludeResetButton,
            pResetButton,
            pKeybinds;

        private AnimBool resetButtonImplemented = new AnimBool();

        private void OnEnable()
        {
            //attach properties
            pButtonPrefab = serializedObject.FindProperty("buttonPrefab");
            pBaseColour = serializedObject.FindProperty("baseColour");
            pSelectedColour = serializedObject.FindProperty("selectedColour");
            pChangedColour = serializedObject.FindProperty("changedColour");
            pIncludeResetButton = serializedObject.FindProperty("includeResetButton");
            pResetButton = serializedObject.FindProperty("resetButton");
            pKeybinds = serializedObject.FindProperty("keybinds");

            resetButtonImplemented.value = pIncludeResetButton.boolValue; //align bool values
            resetButtonImplemented.valueChanged.AddListener(Repaint); //add repaint method to this bool
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //display box and instructions for button prefab
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                //instructions
                EditorGUILayout.LabelField("The button prefab must follow a specific format:", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("Root object is a Text object - will display action name",
                    EditorStyles.label);
                EditorGUILayout.LabelField("Child object is a Button - will display key currently bound",
                    EditorStyles.label);
                EditorGUI.indentLevel--;

                EditorGUILayout.PropertyField(pButtonPrefab);
            }
            EditorGUILayout.EndVertical();

            //display box for choosing colours
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.LabelField("Colours to Show Modified Keybinds", EditorStyles.boldLabel);

                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(pBaseColour);
                EditorGUILayout.PropertyField(pSelectedColour);
                EditorGUILayout.PropertyField(pChangedColour);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

            //optional reset button
            EditorGUILayout.PropertyField(pIncludeResetButton);
            resetButtonImplemented.target = pIncludeResetButton.boolValue;
            if (EditorGUILayout.BeginFadeGroup(resetButtonImplemented.faded))
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(pResetButton);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndFadeGroup();

            //display box for choosing colours
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(pKeybinds);
            }
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif

    #endregion
}