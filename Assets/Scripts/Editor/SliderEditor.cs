using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UI;


[CustomEditor(typeof(ScoreTextManager))]
[CanEditMultipleObjects]
public class ScoreTextEditor : Editor
{

}

[CustomEditor(typeof(RespawnManager))]
[CanEditMultipleObjects]
public class RespawnManagerEditor : Editor
{

}

[CustomEditor(typeof(LivesTextManager))]
[CanEditMultipleObjects]
public class LivesEditor : Editor
{

}

[CustomEditor(typeof(SliderController))]
[CanEditMultipleObjects]
public class SliderEditor : Editor
{
    //string[] funcChoices = new[] { "", "meow" };
    //int curChoiceInd = 0, maxChoiceInd = 0;

    //string[] compChoices = new[] { "", "mew" };
    //int compChoiceInd = 0;

    //public override void OnInspectorGUI()
    //{
    //    DrawDefaultInspector();

    //    SliderController tmp = (SliderController)target;

    //    curChoiceInd = tmp.curInd;
    //    maxChoiceInd = tmp.maxInd;
    //    compChoiceInd = tmp.typeInd;

    //    List<string> choiceList = new List<string>();

    //    Component[] components = tmp.targ.GetComponents<Component>();
    //    foreach (Component comp in components)
    //    {
    //        choiceList.Add(comp.GetType().ToString());
    //    }
    //    compChoices = choiceList.ToArray();
    //    compChoiceInd = EditorGUILayout.Popup("Type: ", compChoiceInd, compChoices);
    //    choiceList.Clear();
    //    tmp.type = compChoices[compChoiceInd];
    //    MethodInfo[] methods = Type.GetType(tmp.type).GetMethods();
    //    foreach (MethodInfo method in methods)
    //    {
    //        ParameterInfo[] p = method.GetParameters();
    //        if (p.Length == 0 && (method.ReturnType == typeof(float) || method.ReturnType == typeof(int)))
    //        {
    //            choiceList.Add(method.Name);
    //        }
    //    }
    //    funcChoices = choiceList.ToArray();

    //    curChoiceInd = EditorGUILayout.Popup("Current: ", curChoiceInd, funcChoices);
    //    maxChoiceInd = EditorGUILayout.Popup("Maximum: ", maxChoiceInd, funcChoices);

    //    tmp.curChoice = funcChoices[curChoiceInd];
    //    tmp.maxChoice = funcChoices[maxChoiceInd];
    //    tmp.curInd = curChoiceInd;
    //    tmp.maxInd = maxChoiceInd;
    //    tmp.typeInd = compChoiceInd;

    //    EditorUtility.SetDirty(target);
    //}


}
