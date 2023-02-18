using UnityEngine;
using UnityEngine.UI;

public class LevelButtons : MonoBehaviour {

    public GameObject[] scoreText, button;

    private string[] prefKeyHS, prefKeyUnlocked;

	void Start ()
    {
        prefKeyHS = new string[4];
        prefKeyUnlocked = new string[4];

        for (int i = 0; i < prefKeyHS.Length; i++)
        {
            prefKeyHS[i] = "HighScore" + (i + 1).ToString();
        }

        for (int i = 0; i < prefKeyUnlocked.Length; i++)
        {
            prefKeyUnlocked[i] = "Unlocked" + (i + 1).ToString();
        }

        PlayerPrefs.SetInt("Unlocked1", 1);
    }

    void Update ()
    {
        for (int i = 0; i < scoreText.Length; i++)
        {
            scoreText[i].GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt(prefKeyHS[i], 0);
        }

        for (int i = 0; i < button.Length; i++)
        {
            if (PlayerPrefs.GetInt(prefKeyUnlocked[i], 0) == 1)
                button[i].GetComponent<Button>().interactable = true;
            else
                button[i].GetComponent<Button>().interactable = false;
        }
    }

    public void ResetProgress()
    {
        for (int i = 0; i < prefKeyHS.Length; i++)
        {
            PlayerPrefs.SetInt(prefKeyHS[i], 0);
            PlayerPrefs.SetInt(prefKeyUnlocked[i], 0);
        }
        PlayerPrefs.SetInt(prefKeyUnlocked[0], 1);

        PlayerPrefs.SetInt("UnlockMultiplayer", 0);
        PlayerPrefs.SetInt("multiplayer", 0);
    }
}
