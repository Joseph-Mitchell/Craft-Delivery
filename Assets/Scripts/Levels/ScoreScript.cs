using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public GameObject scoreText, highScoreText, p1ScoreText, p2ScoreText, p1Finish, p2Finish;
    public GameObject[] p1calculationText, p2calculationText;

    public static float time;
    public static bool timeCalc1, timeCalc2;
    public static int p1Score, p2Score, p1CargoDelivered, p2CargoDelivered;

    public static int maxTime, cargoWorth;
    public int maxTimeEditor, cargoWorthEditor, levelNum;

    private int p1TimeRemaining, p2TimeRemaining;

    private string prefKeyHS;

	void Start ()
    {
        if (PlayerPrefs.GetInt("multiplayer") == 1)
        {
            scoreText.SetActive(false);
            p1ScoreText.SetActive(true);
            p2ScoreText.SetActive(true);

            for(int i = 0; i < p2calculationText.Length; i++)
            {
                p2calculationText[i].SetActive(true);
            }
        }
        else
        {
            scoreText.SetActive(true);
            p1ScoreText.SetActive(false);
            p2ScoreText.SetActive(false);

            for (int i = 0; i < p2calculationText.Length; i++)
            {
                p2calculationText[i].SetActive(false);
            }
        }

        time = 0;
        p1Score = 0;
        p2Score = 0;
        p1CargoDelivered = 0;
        p2CargoDelivered = 0;

        maxTime = maxTimeEditor;
        cargoWorth = cargoWorthEditor;

        prefKeyHS = "HighScore" + levelNum;
	}
	
	void Update ()
    {
        if (Time.timeScale == 1)
            time += Time.deltaTime;

        if (timeCalc1)
        {
            p1TimeRemaining = maxTime - (int)Mathf.Floor(time);
            if (p1TimeRemaining < -cargoWorth)
                p1TimeRemaining = -cargoWorth;
            p1Score = p1CargoDelivered * (cargoWorth + p1TimeRemaining);
            timeCalc1 = false;
        }
        if (timeCalc2)
        {
            p2TimeRemaining = maxTime - (int)Mathf.Floor(time);
            if (p2TimeRemaining < -cargoWorth)
                p2TimeRemaining = -cargoWorth;
            p2Score = p2CargoDelivered * (cargoWorth + p2TimeRemaining);
            timeCalc2 = false;
        }

        if (p1Score > PlayerPrefs.GetInt(prefKeyHS, 0))
        {
            PlayerPrefs.SetInt(prefKeyHS, p1Score);
        }
        if (p2Score > PlayerPrefs.GetInt(prefKeyHS, 0))
        {
            PlayerPrefs.SetInt(prefKeyHS, p2Score);
        }

        p1calculationText[0].GetComponent<Text>().text = "Time Remaining: " + p1TimeRemaining;
        p1calculationText[1].GetComponent<Text>().text = "Cargo Delivered: " + p1CargoDelivered + " (x " + cargoWorth + ")";
        p1calculationText[2].GetComponent<Text>().text = "" + p1CargoDelivered + " x (" + cargoWorth + " + " + p1TimeRemaining + ") = " + p1Score;

        p2calculationText[0].GetComponent<Text>().text = "Time Remaining: " + p2TimeRemaining;
        p2calculationText[1].GetComponent<Text>().text = "Cargo Delivered: " + p2CargoDelivered + " (x " + cargoWorth + ")";
        p2calculationText[2].GetComponent<Text>().text = "" + p2CargoDelivered + " x (" + cargoWorth + " + " + p2TimeRemaining + ") = " + p2Score;

        scoreText.GetComponent<Text>().text = "Score: " + p1Score;
        highScoreText.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt(prefKeyHS, 0);
        p1ScoreText.GetComponent<Text>().text = "Player 1: " + p1Score;
        p2ScoreText.GetComponent<Text>().text = "Player 2: " + p2Score;
    }
}
