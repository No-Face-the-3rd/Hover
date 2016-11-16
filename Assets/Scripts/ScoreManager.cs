using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
    private float score = 0.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public float getScore()
    {
        return score;
    }

    public void setScore(float value)
    {
        score = value;
    }

    public void addScore(float value)
    {
        score += value;
    }
}
