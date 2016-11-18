using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager manager;

    private string playerName = "";

	// Use this for initialization
	void Start () {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(manager != this)
        {
            Destroy(gameObject);
        }


	}

	// Update is called once per frame
	void Update () {
	
	}

    public void setPlayerName(string name)
    {
        playerName = name;
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1;
    }
}
