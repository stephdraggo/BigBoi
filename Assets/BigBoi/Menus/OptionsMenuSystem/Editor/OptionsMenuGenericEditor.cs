using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BigBoi.Menus.OptionsMenuSystem
{
    // public class OptionsMenuGenericEditor : Editor
    // {
    //     
    // }

    [CustomPropertyDrawer(typeof(OptionsMethod))]
    public class OptionsMethodEditor : CustPropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty pName = property.FindPropertyRelative("name");
            SerializedProperty pType = property.FindPropertyRelative("type");
            SerializedProperty pObject = property.FindPropertyRelative("uiObject");
            SerializedProperty pButton = property.FindPropertyRelative("buttonEvent");
            SerializedProperty pSlider = property.FindPropertyRelative("sliderEvent");
            SerializedProperty pToggle = property.FindPropertyRelative("toggleEvent");
            SerializedProperty pDropdown = property.FindPropertyRelative("dropdownEvent");

            EditorGUI.BeginProperty(position, label, property);
            {
                Rect pos = position;
                pos.height = EditorGUIUtility.singleLineHeight;

                EditorGUI.BeginFoldoutHeaderGroup(pos, true, pName.stringValue);
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                RenderProperty(pName, ref pos);
                RenderProperty(pType, ref pos);
                RenderProperty(pObject, ref pos);

                switch ((UIType) pType.enumValueIndex) {
                    case UIType.Button:
                        RenderProperty(pButton, ref pos);
                        break;
                    case UIType.Slider:
                        RenderProperty(pSlider, ref pos);
                        break;
                    case UIType.Toggle:
                        RenderProperty(pToggle, ref pos);
                        break;
                    case UIType.Dropdown:
                        RenderProperty(pDropdown, ref pos);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                EditorGUI.EndFoldoutHeaderGroup();
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            SerializedProperty pType = property.FindPropertyRelative("type");
            string eventName = pType.enumNames[pType.enumValueIndex].ToLower() + "Event";
            SerializedProperty pEvent = property.FindPropertyRelative(eventName);

            return EditorGUI.GetPropertyHeight(pEvent) +
                (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 3;
        }

        private static void RenderProperty(SerializedProperty property, ref Rect position) {
            EditorGUI.PropertyField(position, property);

            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }
    }
}