using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {

    private bool buttonDownOld = false;
    public GameObject pausePanel, levelCompletePanel, splitter;

    void Start()
    {
        if (PlayerPrefs.GetInt("multiplayer") == 1)
            splitter.SetActive(true);
        else
            splitter.SetActive(false);
    }

    void Update ()
    {
        if (Input.GetButtonDown("Pause") && !levelCompletePanel.activeSelf)
        {
            if (!buttonDownOld)
            {
                TogglePause();
            }
            buttonDownOld = true;
        }
        else
            buttonDownOld = false;
    }

    public void TogglePause()
    {
        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }
}
