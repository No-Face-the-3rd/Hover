using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour {
    public GameObject startCanvas;
    public GameObject scoreCanvas;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void enterName()
    {
        startCanvas.SetActive(true);
    }

    public void cancelName()
    {
        InputField field = startCanvas.GetComponentInChildren<InputField>();
        field.text = "";
        startCanvas.SetActive(false);
    }

    public void exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void enterScore()
    {
        Text scores = scoreCanvas.transform.FindChild("ScoreText").GetComponent<Text>();
        scores.text = scoreHi.scoresToString(HighScoreManager.manager.getScores());
        scoreCanvas.SetActive(true);
    }
    public void cancelScore()
    {
        scoreCanvas.SetActive(false);
    }

    public void startGame()
    {
        InputField field = startCanvas.GetComponentInChildren<InputField>();
        GameManager.manager.setPlayerName(field.text);
        GameManager.manager.setLives(5);
        GameManager.manager.loadScene("Main");
    }
}
