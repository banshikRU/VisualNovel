using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
//[CustomEditor(typeof(PatternScriptableObject))]
public class QuizSettings : Editor
{
    public override void OnInspectorGUI()
    {

        PatternScriptableObject patternScriptableObject = (PatternScriptableObject)target;
        patternScriptableObject.isQuiz = EditorGUILayout.Toggle("IsQuiz", patternScriptableObject.isQuiz);
        if (patternScriptableObject.isQuiz)
        {
            EditorGUILayout.LabelField("Quiz Variants:");
            if (patternScriptableObject.quizVariants == null) { patternScriptableObject.quizVariants = new List<String>(); }
            for (int i = 0; i < patternScriptableObject.quizVariants.Count; i++)
            {
                patternScriptableObject.quizVariants[i] = EditorGUILayout.TextField($"Element {i + 1}", patternScriptableObject.quizVariants[i]);

            }
            if (GUILayout.Button("Add Element"))
            {
                patternScriptableObject.quizVariants.Add(string.Empty);
            }
            if (patternScriptableObject.quizVariants.Count > 0 && GUILayout.Button("Remove Last Element"))
            {
                patternScriptableObject.quizVariants.RemoveAt(patternScriptableObject.quizVariants.Count - 1);
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Scriptable Objects:");
            for (int i = 0; i < patternScriptableObject.quizPatterns.Count; i++)
            {
                patternScriptableObject.quizPatterns[i] = (PatternScriptableObject)EditorGUILayout.ObjectField($"SO {i + 1}", patternScriptableObject.quizPatterns[i], typeof(PatternScriptableObject), false);
            }
            if (GUILayout.Button("Add Scriptable Object"))
            {
                patternScriptableObject.quizPatterns.Add(null);
            }
            if (patternScriptableObject.quizPatterns.Count > 0 && GUILayout.Button("Remove Last Scriptable Object"))
            {
                patternScriptableObject.quizPatterns.RemoveAt(patternScriptableObject.quizPatterns.Count - 1);
            }
        }
        else
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("DialogSettings");
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(patternScriptableObject);
        }
    }
    
}