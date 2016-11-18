using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    private bool paused = false;
    public GameObject pauseCanvas;
    public GameObject loseCanvas;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
        }
    }

    public void enterLose()
    {
        Time.timeScale = 0;
        loseCanvas.SetActive(true);
    }

    public void togglePause()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
        }
    }

    public void exitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void enterMainMenu()
    {
        GameManager.manager.loadScene("MainMenu");
    }

}
