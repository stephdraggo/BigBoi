using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace BigBoi.Menus.OptionsMenuSystem
{
    [Serializable]
    [CreateAssetMenu(menuName = "BigBoi/Menus/Options", fileName = "new options")]
    public class OptionsMenuGeneric : ScriptableObject
    {
        [SerializeField]
        private List<OptionsMethod> availableMethods = new List<OptionsMethod>();

        private void OnValidate() {
            Setup();
        }

        #region ValidateMethods

        private void Setup() {
            foreach (OptionsMethod method in availableMethods) {
                bool valid = true;
                switch (method.type) {
                    case UIType.Button:
                        if (method.uiObject.TryGetComponent(out Button button)) {
                            Setup(button,method);
                        }
                        else valid = false;

                        break;
                    case UIType.Slider:
                        if (method.uiObject.TryGetComponent(out Slider slider)) {
                            Setup(slider,method);
                        }
                        else valid = false;

                        break;
                    case UIType.Toggle:
                        if (method.uiObject.TryGetComponent(out Toggle toggle)) {
                            Setup(toggle,method);
                        }
                        else valid = false;

                        break;
                    case UIType.Dropdown:
                        if (method.uiObject.TryGetComponent(out Dropdown dropdown)) {
                            Setup(dropdown,method);
                        }
                        else valid = false;

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (!valid) Debug.LogWarning($"UI object does not contain {method.type} component");
            }
        }

        private void Setup(Button button, OptionsMethod method) {
            button.onClick = method.buttonEvent;
        }

        private void Setup(Slider slider, OptionsMethod method) {
            slider.onValueChanged = method.sliderEvent;
        }

        private void Setup(Toggle toggle, OptionsMethod method) {
            toggle.onValueChanged = method.toggleEvent;
        }

        private void Setup(Dropdown dropdown, OptionsMethod method) {
            dropdown.onValueChanged = method.dropdownEvent;
        }

        #endregion
    }

    [Serializable]
    public class OptionsMethod
    {
        public string name = "new method";

        public UIType type;

        public GameObject uiObject;

        public Button.ButtonClickedEvent buttonEvent = new Button.ButtonClickedEvent();
        public Slider.SliderEvent sliderEvent = new Slider.SliderEvent();
        public Toggle.ToggleEvent toggleEvent = new Toggle.ToggleEvent();
        public Dropdown.DropdownEvent dropdownEvent = new Dropdown.DropdownEvent();
    }


    public enum UIType
    {
        Button,
        Slider,
        Toggle,
        Dropdown,
    }

    public static class Methods
    {
        public static void Quit() {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }
    }
}