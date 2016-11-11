using UnityEngine;
using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

[CustomPropertyDrawer(typeof(ComponentFunc))]
public class ComponentFuncEditor : PropertyDrawer
{
    string nameLabel = "Field Label";
    GameObject targ;
    string[] returnTypeChoices;
    string returnType;
    int returnTypeMask;
    string[] funcChoices = new[] { "", "meow" };
    int funcChoiceInd = 0;
    string[] compChoices = new[] { "", "mew" };
    int compChoiceInd = 0;
    string component = "";
    string function = "";
    bool foldout = false;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //store current values
        compChoiceInd = property.FindPropertyRelative("compInd").intValue;
        funcChoiceInd = property.FindPropertyRelative("choiceInd").intValue;
        targ = (GameObject)property.FindPropertyRelative("target").objectReferenceValue;
        returnTypeMask = property.FindPropertyRelative("returnTypeMask").intValue;
        returnType = property.FindPropertyRelative("returnType").stringValue;
        component = property.FindPropertyRelative("component").stringValue;
        function = property.FindPropertyRelative("choice").stringValue;
        foldout = property.FindPropertyRelative("foldout").boolValue;

        EditorGUILayout.PropertyField(property.FindPropertyRelative("label"));
        nameLabel = property.FindPropertyRelative("label").stringValue;
        nameLabel = String.Equals(nameLabel, "") ? "Field Label" : nameLabel;
        property.FindPropertyRelative("label").stringValue = nameLabel;

        //create string list for choices
        List<string> choiceList = new List<String>();
        List<string> choiceQualified = new List<String>();

        foldout = EditorGUILayout.Foldout(foldout, nameLabel);

        //do label
        //choose gameobject
        if (foldout)
        {

            targ = (GameObject)EditorGUILayout.ObjectField(nameLabel + " Target ", targ, typeof(GameObject), true);

            if (targ != null)
            {
                //choose component
                Component[] components = targ.GetComponents<Component>();
                foreach (Component comp in components)
                {
                    choiceList.Add(comp.GetType().ToString());
                    choiceQualified.Add(comp.GetType().AssemblyQualifiedName);
                }
                compChoices = choiceList.ToArray();
                compChoiceInd = EditorGUILayout.Popup(nameLabel + " Component", compChoiceInd, compChoices);
                component = compChoiceInd >= 0 ? compChoices[compChoiceInd] : component;
                compChoices = choiceQualified.ToArray();
                string compQual = compChoiceInd >= 0 ? compChoices[compChoiceInd] : component;
                if (compChoiceInd >= 0)
                {
                    MethodInfo[] methods = Type.GetType(choiceQualified.ToArray()[compChoiceInd]).GetMethods();

                    choiceList.Clear();
                    choiceQualified.Clear();

                    foreach (MethodInfo method in methods)
                    {
                        ParameterInfo[] p = method.GetParameters();
                        string retType = method.ReturnType.ToString();
                        if (p.Length == 0 && !choiceList.Exists(element => String.Equals(element, retType)) && !String.Equals(retType, "T") && !String.Equals(retType, "T[]"))
                        {
                            choiceList.Add(retType);
                            choiceQualified.Add(method.ReturnType.AssemblyQualifiedName);
                        }
                    }
                    returnTypeChoices = choiceList.ToArray();
                    returnTypeMask = EditorGUILayout.MaskField(nameLabel + " Return Type", returnTypeMask, returnTypeChoices);
                    string[] typeQual = choiceQualified.ToArray();

                    choiceList.Clear();
                    choiceQualified.Clear();

                    if (returnTypeMask < 0)
                    {
                        returnTypeMask = (1 << returnTypeChoices.Length) - 1;
                    }

                    for (int j = 0; j < methods.Length; j++)
                    {
                        MethodInfo method = methods[j];
                        ParameterInfo[] p = method.GetParameters();
                        if (p.Length == 0 && returnTypeMask > 0 && (method.IsPublic || method.IsFamily))
                        {
                            for (int i = 0; i < returnTypeChoices.Length; i++)
                            {
                                if ((returnTypeMask & (1 << i)) > 0)
                                {
                                    if (method.ReturnType == Type.GetType(typeQual[i]))
                                    {
                                        choiceList.Add(method.Name);
                                        choiceQualified.Add(method.ReflectedType.Name);
                                    }
                                }
                            }
                        }
                    }
                    funcChoices = choiceList.ToArray();
                    funcChoiceInd = EditorGUILayout.Popup(nameLabel + " Function", funcChoiceInd, funcChoices);

                    function = funcChoiceInd >= 0 ? funcChoices[funcChoiceInd] : function;

                    returnType = (funcChoiceInd >= 0 && compChoiceInd >= 0) ? Type.GetType(compQual).GetMethod(function).ReturnType.AssemblyQualifiedName : "";

                }

            }
            else
            {
                compChoiceInd = -1;
                funcChoiceInd = -1;
                returnTypeMask = 0;
                returnType = "";
                component = "";
                function = "";
            }
        }

        //store changed values
        property.FindPropertyRelative("compInd").intValue = compChoiceInd;
        property.FindPropertyRelative("choiceInd").intValue = funcChoiceInd;
        property.FindPropertyRelative("target").objectReferenceValue = targ;
        property.FindPropertyRelative("returnTypeMask").intValue = returnTypeMask;
        property.FindPropertyRelative("returnType").stringValue = returnType;
        property.FindPropertyRelative("component").stringValue = component;
        property.FindPropertyRelative("choice").stringValue = function;
        property.FindPropertyRelative("foldout").boolValue = foldout;


    }

}

