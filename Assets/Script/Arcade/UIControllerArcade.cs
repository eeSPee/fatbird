using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerArcade : UIController
{
    Text ScoreCounter;
    Text ComboCounter;
    GameObject ComboHelp;
    GameObject GameOverScreen;
    public override void Awake()
    {
        base.Awake();
        ScoreCounter = transform.Find("Score Counter").GetComponent<Text>();
        ComboCounter = transform.Find("Combo Counter").GetComponent<Text>();
        ComboHelp = transform.Find("Combo Title").gameObject;

        GameOverScreen = transform.Find("Game Over").gameObject;
    }
    public override void DisableGameOverScreen()
    {
        GameOverScreen.SetActive(false);
    }
    public override void Update()
    {
        ScoreCounter.text = ScoreController.main.Score.ToString();

        ComboCounter.gameObject.SetActive(ScoreController.main.Combo > 0);
        ComboHelp.gameObject.SetActive(ScoreController.main.Combo > 0);
        ComboCounter.text = "x" + ScoreController.main.Combo;
    }
    static float HighScore = 0;
    public override void EnableGameOverScreen(bool victory)
    {
        GameOverScreen.SetActive(true);
        HighScore = Mathf.Max(HighScore, ScoreController.main.Score);
        GameOverScreen.transform.Find("Hiscore Counter").GetComponent<Text>().text = HighScore + "!";
    }
}
