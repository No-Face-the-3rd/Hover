using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

using System.Reflection;

[System.Serializable]
public class ComponentFuncs
{
    public ComponentFunc[] functions;

    public object callFunc(int ind)
    {
        return functions[ind].callFunc();
    }

    public Type getType(int ind)
    {
        return functions[ind].GetType();
    }
}

[System.Serializable]
public class ComponentFunc
{
    public string label = "Field Label";
    public GameObject target;
    public string component;
    public int compInd;
    public string returnType;
    public int returnTypeMask;
    public string choice;
    public int choiceInd;
    public bool foldout;

    public object callFunc()
    {
        if (String.Equals(component, "") || String.Equals(choice, "") || target == null)
        {
            throw new Exception("Part not Chosen");
        }
        var tmp = target.GetComponent(component);

        MethodInfo method = Type.GetType(component).GetMethod(choice);
        object outCur = method.Invoke(tmp, null);


        return outCur;

    }

    public Type getType()
    {
        return Type.GetType(returnType);
    }
}

public class SliderController : MonoBehaviour {

    public Slider slider;
    public ComponentFunc percentFunction;




    // Use this for initialization
    void Start () {
        slider.maxValue = 1.0f;
	}

    // Update is called once per frame
    void Update()
    {

        slider.value = (float)percentFunction.callFunc();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}


