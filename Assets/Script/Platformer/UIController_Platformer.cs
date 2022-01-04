using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController_Platformer : UIController
{
    OnScreenTimer TimerDisplay;
    GameObject WinScreen;
    GameObject LoseScreen;
    GameObject LevelSelScreen;
    GameObject BackButton;
    GameObject FrontButton;
    public override void Awake()
    {
        base.Awake();
        TimerDisplay = GetComponentInChildren<OnScreenTimer>();
        TimerDisplay.gameObject.SetActive(false);
        WinScreen = transform.Find("Game Complete").gameObject;
        LoseScreen = transform.Find("Game Over").gameObject;
        LevelSelScreen = transform.Find("LevelSelect").gameObject;
        Transform LevelSelParent = LevelSelScreen.transform.Find("Level Select");
        FrontButton = LevelSelParent.Find("Right Btn").gameObject;
        BackButton = LevelSelParent.Find("Left Btn").gameObject;
    }
    public void EnableLevelSelect(bool value)
    {
        OnScreenBugCounter.main.SetEnabled(false);
        EnableDisableTimer( false);
        LevelSelScreen.SetActive(value);
        PlayerController.main.enabled = !value;
        Tutorial.SetActive(!value);
        //SelectLevel( PlayerPrefs.GetInt(LevelController.main.GetLevelName() + " LastCheckPoint"));
    }
    public override void DisableTutorial()
    {
        base.DisableTutorial();
        OnScreenBugCounter.main.SetEnabled(true);
        EnableDisableTimer(true);
    }
    public void HandlePlayerChoseCheckpoint(bool forward)
    {
        LevelController_Platformer lvc = (LevelController_Platformer)LevelController.main;
        if (forward)
        {
            lvc. JumpToCheckpoint(lvc.currentCheckpoint + 1);
        }
        else 
        {
            if (lvc.currentCheckpoint == 0)
                return;
            lvc.JumpToCheckpoint(lvc.currentCheckpoint - 1);
        }

        BackButton.GetComponent<Button>().interactable = lvc.currentCheckpoint > 0;
        FrontButton.GetComponent<Button>().interactable = lvc.currentCheckpoint < lvc.GetMaxCheckPoints();
        LevelSelScreen.transform.Find("Level Select").Find("CheckpointID").GetComponent<Text>().text = "" + (1 + lvc.currentCheckpoint);
    }
    public override void DisableGameOverScreen()
    {
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }
    public void EnableDisableTimer(bool value)
    {
        TimerDisplay.gameObject.SetActive(value);
    }
    public override void EnableGameOverScreen(bool victory)
    {
        WinScreen.SetActive(victory);
        LoseScreen.SetActive(!victory);
        LevelSelScreen.SetActive(false);
        OnScreenBugCounter.main.SetEnabled( !victory);
        EnableDisableTimer(!victory);
    }
    protected override void Start()
    {
        base.Start();
        Tutorial.SetActive(true);
        EnableDisablePauseMenu(false);
        EnableLevelSelect(true);
    }
}