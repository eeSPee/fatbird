using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController main;
    protected virtual void Awake()
    {
        main = this;
    }
    protected virtual void Update()
    {
        if ( !GameOver && Input.GetKeyUp(KeyCode.P))
        {
            PauseUnpause(!GamePaused);
        }
        if ( (GameRunning || GameOver) && Input.GetKeyUp(KeyCode.R))
        {
            ResetGame();
        }
    }
    protected virtual void Start()
    {
    }
    public virtual void StartTheGame()
    {
        PauseUnpause(false);
        UIController.main.DisableTutorial();
        GameRunning = true;
        StartTime = Time.time;
    }
    public virtual void EndTheGame(bool victory)
    {
        PauseUnpause(false);
        UIController.main.EnableGameOverScreen(victory);
        GameOver = true;
        GameRunning = false;
    }
    public virtual void ResetGame()
    {
        EndTheGame(false);
        GameOver = false;
        FatBirdController.main.Reset();
        UIController.main.DisableGameOverScreen();
        PauseUnpause(false);
    }
    bool GameRunning = false;
    bool GameOver = false;
    public bool IsGameOver()
    {
        return GameOver;
    }
    public bool IsGameRunning()
    {
        return GameRunning;
    }
    float StartTime = 0;
    public virtual float GetGameTime()
    {
        return Time.time - StartTime;
    }
    bool GamePaused = false;
    public virtual void PauseUnpause(bool pause)
    {
        GamePaused = pause;
        Time.timeScale = pause ? 0 : 1f;
        UIController.main.EnableDisablePauseMenu(pause);
    }
    private void OnDisable()
    {
        PauseUnpause(false);
    }
}
