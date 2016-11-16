using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreTextManager : MonoBehaviour {
    public Text scoreText;

    public ComponentFunc scoreFunction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + (Mathf.FloorToInt((float)scoreFunction.callFunc())).ToString();
	}
}
