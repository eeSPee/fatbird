using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    Text ScoreCounter;
    Text ComboCounter;
    GameObject ComboHelp;
    GameObject Tutorial;
    GameObject GameOverScreen;

    public static UIController main;
    private void Awake()
    {
        main = this;
        ScoreCounter = transform.Find("Score Counter").GetComponent<Text>();
        ComboCounter = transform.Find("Combo Counter").GetComponent<Text>();
        ComboHelp = transform.Find("Combo Title").gameObject;

        Tutorial = transform.Find("Tutorial").gameObject;
        GameOverScreen = transform.Find("Game Over").gameObject;
    }
    private void Start()
    {
        UpdateScore();
        DisableGameOverScreen();
    }

    public void DisableTutorial()
    {
        Tutorial.SetActive(false);
    }
    float HighScore = 0;
    public void EnableGameOverScreen()
    {
        GameOverScreen.SetActive(true);
        HighScore = Mathf.Max(HighScore, ScoreController.main.Score);
        GameOverScreen.transform.Find("Hiscore Counter").GetComponent<Text>().text = HighScore+"!";
    }
    public void DisableGameOverScreen()
    {
        GameOverScreen.SetActive(false);
    }
    public void UpdateScore()
    {
        ScoreCounter.text = ScoreController.main.Score.ToString();

        ComboCounter.gameObject.SetActive(ScoreController.main.Combo>1);
        ComboHelp.gameObject.SetActive(ScoreController.main.Combo>1);
        ComboCounter.text = "x"+ScoreController.main.Combo;
    }
}
