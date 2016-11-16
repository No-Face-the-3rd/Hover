using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LivesTextManager : MonoBehaviour {
    public Text livesText;

    public ComponentFunc livesFunc;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        livesText.text = "Lives: x" + ((int)livesFunc.callFunc()).ToString();
	}
}
