using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerArcade : UIController
{
    UIScoreDisplay ScoreDisplay;
    UIGameOverScreen GameOverScreen;
    public override void Awake()
    {
        base.Awake();

        ScoreDisplay = GetComponentInChildren<UIScoreDisplay>();
        GameOverScreen = GetComponentInChildren<UIGameOverScreen>();
    }
    public override void DisableGameOverScreen()
    {
        GameOverScreen.gameObject.SetActive(false);
        ScoreDisplay.gameObject.SetActive(true);
    }
    public override void EnableGameOverScreen(bool victory)
    {
        ScoreDisplay.gameObject.SetActive(false);
        GameOverScreen.gameObject.SetActive(true);
    }
}
