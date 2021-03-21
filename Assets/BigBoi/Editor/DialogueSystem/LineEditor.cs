using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

namespace BigBoi.DialogueSystem
{
    [CustomEditor(typeof(Line))]

    public class LineEditor : Editor
    {
        private SerializedProperty pPerson, pExpression, pDialogueText, pResponseType, pLabel1, pLabel2, pTarget1, pTarget2;

        private AnimBool responseIsChoice = new AnimBool();

        private bool respondChoice
        {
            get
            {
                if (pResponseType.enumValueIndex == (int)ActionTypes.JumpTo)
                {
                    return true;
                }
                return false;
            }
        }

        private void OnEnable()
        {
            //attach properties
            pPerson = serializedObject.FindProperty("person");
            pExpression = serializedObject.FindProperty("expression");
            pDialogueText = serializedObject.FindProperty("dialogueText");
            pResponseType = serializedObject.FindProperty("responseType");
            pLabel1 = serializedObject.FindProperty("label");
            pLabel2 = serializedObject.FindProperty("label2");
            pTarget1 = serializedObject.FindProperty("target");
            pTarget2 = serializedObject.FindProperty("target2");



            //it is not working
            responseIsChoice.value = respondChoice; //align bool to specific response type
            responseIsChoice.valueChanged.AddListener(Repaint); //add repaint method to this bool
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(pPerson);
                EditorGUILayout.PropertyField(pExpression);
                EditorGUILayout.PropertyField(pDialogueText);
                EditorGUILayout.PropertyField(pResponseType);
            }
            EditorGUILayout.EndVertical();

            //response options
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.LabelField("Details for response buttons.");
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(pLabel1);

                //this section should be hidden unless the the response type is "choice"
                //it is currently always being shown
                responseIsChoice.target = respondChoice;
                EditorGUILayout.BeginFadeGroup(responseIsChoice.faded);
                {
                    EditorGUILayout.PropertyField(pTarget1);
                    EditorGUILayout.PropertyField(pLabel2);
                    EditorGUILayout.PropertyField(pTarget2);
                }
                EditorGUILayout.EndFadeGroup();

                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();



            serializedObject.ApplyModifiedProperties();
        }
    }
}