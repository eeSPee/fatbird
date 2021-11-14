using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerPlatformer : UIController
{
    Text TimerDisplay;
    GameObject WinScreen;
    GameObject LoseScreen;
    public override void Awake()
    {
        base.Awake();
        TimerDisplay = transform.Find("Time Display").GetComponent<Text>();
        WinScreen = transform.Find("Game Complete").gameObject;
        LoseScreen = transform.Find("Game Over").gameObject;
    }
    public override void DisableGameOverScreen()
    {
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }
    public override void Update()
    {
        TimerDisplay.gameObject.SetActive(LevelController.main.IsGameRunning());
        if (LevelController.main.IsGameRunning())
        {
            float gameTime = LevelController.main.GetGameTime();
            float mins = (Mathf.Round(gameTime / 60));
            float secs = Mathf.Ceil(gameTime % 60);

            TimerDisplay.text = mins + ":" + (secs<10 ? "0" : "")+ secs;
        }
    }
    public override void EnableGameOverScreen(bool victory)
    {
        WinScreen.SetActive(victory);
        LoseScreen.SetActive(!victory);
    }
}
