using UnityEditor;
using UnityEditor.AnimatedValues;

namespace BigBoi.DialogueSystem
{
    [CustomEditor(typeof(Person))]
    public class PersonEditor : Editor
    {
        private SerializedProperty pKnown, pSeen, pHasAlias, pAlias, pPictures;

        private AnimBool HasAlias = new AnimBool();

        private void OnEnable()
        {
            pKnown = serializedObject.FindProperty("known");
            pSeen = serializedObject.FindProperty("seen");
            pHasAlias = serializedObject.FindProperty("hasAlias");
            pAlias = serializedObject.FindProperty("alias");
            pPictures = serializedObject.FindProperty("pictures");

            HasAlias.value = pHasAlias.boolValue;
            HasAlias.valueChanged.AddListener(Repaint);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(pKnown);
            EditorGUILayout.PropertyField(pSeen);
            EditorGUILayout.PropertyField(pHasAlias);

            //reveal alias creation array if aliases enabled
            HasAlias.target = pHasAlias.boolValue;
            if (EditorGUILayout.BeginFadeGroup(HasAlias.faded))
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(pAlias);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFadeGroup();

            EditorGUILayout.PropertyField(pPictures);

            serializedObject.ApplyModifiedProperties();
        }
    }
}