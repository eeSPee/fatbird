using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerPlatformer : UIController
{
    int currentCheckpoint = 0;
    Text TimerDisplay;
    GameObject WinScreen;
    GameObject LoseScreen;
    GameObject LevelSelScreen;
    GameObject BackButton;
    GameObject FrontButton;
    public override void Awake()
    {
        base.Awake();
        TimerDisplay = transform.Find("Time Display").GetComponent<Text>();
        TimerDisplay.gameObject.SetActive(false);
        WinScreen = transform.Find("Game Complete").gameObject;
        LoseScreen = transform.Find("Game Over").gameObject;
        LevelSelScreen = transform.Find("LevelSelect").gameObject;
        FrontButton = LevelSelScreen.transform.Find("Right Btn").gameObject;
        BackButton = LevelSelScreen.transform.Find("Left Btn").gameObject;
    }
    public void EnableLevelSelect(bool value)
    {
        LevelSelScreen.SetActive(value);
        FatBirdController.main.enabled = !value;
        Tutorial.SetActive(!value);
        ShowTimer = false;
        SelectLevel( PlayerPrefs.GetInt(LevelController.main.GetLevelName() + " LastCheckPoint"));
    }
    public void SelectLevel(int nLevel)
    {
        int max = PlayerPrefs.GetInt(LevelController.main.GetLevelName() + " CheckpointProgress");
        nLevel = Mathf.Clamp(nLevel, 0, max);

        BackButton.GetComponent<Button>().interactable = nLevel > 0;
        FrontButton.GetComponent<Button>().interactable = nLevel < max;

        CheckPointController checkpoint = (LevelController.main as PlatformerLevelController).GetCheckpointByID(nLevel);
        if (checkpoint != null)
        {
            currentCheckpoint = nLevel;
            Camera.main.transform.position = new Vector3(checkpoint.transform.position .x, checkpoint.transform.position .y , Camera.main.transform.position .z);
            FatBirdController.main.transform.position = checkpoint.transform.position;
            (FatBirdController.main as FatBirdPlatformer).SetCheckPoint(checkpoint);

            LevelSelScreen.transform.Find("CheckpointID").GetComponent<Text>().text = ""+ (1+nLevel);
        }
    }
    public void HandlePlayerChoseCheckpoint(bool forward)
    {
        if (forward)
        {
            SelectLevel(currentCheckpoint + 1);
        }
        else 
        {
            if (currentCheckpoint == 0)
                return;
            SelectLevel(currentCheckpoint - 1);
        }
    }
    public override void DisableGameOverScreen()
    {
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }
    public bool ShowTimer = true;
    public void Update()
    {
        TimerDisplay.gameObject.SetActive(ShowTimer);
        if (ShowTimer)
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
        LevelSelScreen.SetActive(false);
    }
    protected override void Start()
    {
        base.Start();
        Tutorial.SetActive(false);
        EnableDisablePauseMenu(false);
        EnableLevelSelect(true);
        SelectLevel(currentCheckpoint);
    }
}
