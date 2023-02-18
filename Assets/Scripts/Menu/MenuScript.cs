using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    private enum MenuState
    {
        attract,
        levelSelect
    };

    private MenuState menuState;
    public GameObject attractCanvas, levelSelectCanvas, multiplayerButton, multiplayerButtonCheck, page, loading;

	void Start ()
    {
        menuState = MenuState.attract;
        loading.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (menuState == MenuState.attract)
        {
            attractCanvas.SetActive(true);
            levelSelectCanvas.SetActive(false);
            if (Input.GetButtonDown("Submit"))
            {
                menuState = MenuState.levelSelect;
                page.GetComponent<PageMove>().playAnim = true;
                levelSelectCanvas.SetActive(true);
                levelSelectCanvas.GetComponent<PageMove>().playAnim = true;
            }
        }
        else
        {
            attractCanvas.SetActive(false);
        }

        if (PlayerPrefs.GetInt("UnlockMultiplayer") == 1)
        {
            multiplayerButton.SetActive(true);
        }
        else
        {
            multiplayerButton.SetActive(false);
        }

        if (PlayerPrefs.GetInt("multiplayer") == 1)
        {
            multiplayerButtonCheck.SetActive(true);
        }
        else
        {
            multiplayerButtonCheck.SetActive(false);
        }
    }

    public void LoadLevel(int levelNum)
    {
        loading.SetActive(true);
        levelSelectCanvas.SetActive(false);
        string level = "Level" + levelNum.ToString();
        SceneManager.LoadScene(level);
    }

    public void MultiplayerToggle()
    {
        if (PlayerPrefs.GetInt("multiplayer") == 1)
        {
            PlayerPrefs.SetInt("multiplayer", 0);      
        }
        else
        {
            PlayerPrefs.SetInt("multiplayer", 1);          
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
