using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BigBoi.Editors
{
    public class EditorBase : MonoBehaviour
    {
        
    }

#if UNITY_EDITOR

    [CanEditMultipleObjects]
    public abstract class StaticControllerInfoEditor : Editor
    {

        //protected List<Property> properties;

        protected Dictionary<string, Property> properties;
        
        

        /// <summary>
        /// Base initialises dictionary
        /// </summary>
        protected virtual void OnEnable() {
            properties = new Dictionary<string, Property>();
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorFormatting();

            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Put all the editor display formatting in here.
        /// Gets called in OnInspectorGUI by default.
        /// </summary>
        protected abstract void EditorFormatting();

        /// <summary>
        /// Add a property to the dictionary by passing only it's string name;
        /// </summary>
        /// <param name="propertyName">name of variable to display</param>
        protected void AddProperty(string propertyName) {
            Property newProperty = new Property(serializedObject, propertyName);
            properties.Add(propertyName, newProperty);
        }

        /// <summary>
        /// Serialised property and property name with constructor.  
        /// </summary>
        public struct Property
        {
            public readonly SerializedProperty property;
            public readonly string name;

            public Property(SerializedObject serializedObject, string name) {
                property = serializedObject.FindProperty(name);
                this.name = name.NameProperty();
            }
        }


        
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    //[CustomPropertyDrawer(typeof(KeyBindDefaults))]
    public class KeyBindDefaultsEditor : PropertyDrawer
    {
        //number of field spaces taken by the fields total, set manually here
        //if 3 fields but one takes 2 lines of space, spacesCount should be 1+1+2=4
        private static int spacesCount;

        private Rect localPos;
        private int lineCounter, fieldCounter;

        //one singleLineHeight for each line spaces a given field should take
        //add lineSpacing and previous field's height to next y coordinate
        private float singleLineHeight = EditorGUIUtility.singleLineHeight,
            lineSpacing = EditorGUIUtility.standardVerticalSpacing;

        GUIContent helpBox =
            new GUIContent(
                "Do not change the increment unless you want to start counting from a number other than 1.",
                EditorGUIUtility.IconContent("console.infoicon").image);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty pDefaultKeyName = property.FindPropertyRelative("defaultKeyName");
            SerializedProperty pDoublesIncrement = property.FindPropertyRelative("doublesIncrement");

            EditorGUI.BeginProperty(position, label, property);
            {
                lineCounter = 0;
                fieldCounter = 0;
                localPos = position;

                RenderPropertyField(pDefaultKeyName);
                RenderPropertyField(pDoublesIncrement);
                RenderLabelField(helpBox, EditorStyles.helpBox, 2);
            }
            EditorGUI.EndProperty();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="size">standard line count this field should take up, default 1</param>
        private void RenderPropertyField(SerializedProperty property, int size = 1) {
            IncrementRect(size);
            EditorGUI.PropertyField(localPos, property);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="style"></param>
        /// <param name="size">standard line count this field should take up, default 1</param>
        private void RenderLabelField(GUIContent label, GUIStyle style, int size = 1) {
            IncrementRect(size);
            EditorGUI.LabelField(localPos, label, style);
        }

        private void IncrementRect(int size) {
            fieldCounter++;
            localPos.y = singleLineHeight * lineCounter + lineSpacing * (fieldCounter + 2);
            localPos.height = singleLineHeight * size;
            lineCounter += size;
            if (spacesCount < lineCounter) {
                spacesCount = lineCounter;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return base.GetPropertyHeight(property, label) * spacesCount + lineSpacing;
        }
    }

#endif
}