using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController main;
    protected virtual void Awake()
    {
        main = this;
#if UNITY_ANDROID
        Application.targetFrameRate = 50;
#endif
    }
    protected virtual void Update()
    {
#if UNITY_ANDROID

        if (!GameOver && Input.GetKeyUp(KeyCode.Escape))
        {
            PauseUnpause(!GamePaused);
        }

#else
        if ( !GameOver && Input.GetKeyUp(KeyCode.P))
        {
            PauseUnpause(!GamePaused);
        }
        if ( (GameRunning || GameOver) && Input.GetKeyUp(KeyCode.R))
        {
            ResetGame(LevelComplete);
        }
#endif
    }
    protected virtual void Start()
    {
    }
    public virtual void StartTheGame()
    {
        PauseUnpause(false);
        UIController.main.DisableTutorial();
        GameRunning = true;
        if (StartTime == 0)
        StartTime = Time.time;
    }
    public virtual void EndTheGame(bool victory)
    {
        PauseUnpause(false);
        UIController.main.EnableGameOverScreen(victory);
        GameOver = true;
        GameRunning = false;
        LevelComplete = victory;
    }
    public virtual void ResetGame(bool hardReset)
    {
        EndTheGame(false);
        GameOver = false;
        PlayerController.main.OnLevelReset();
        UIController.main.DisableGameOverScreen();
        PauseUnpause(false);
        if (hardReset)
            StartTime = Time.time;
    }
    bool GameRunning = false;
    bool GameOver = false;
    bool LevelComplete = false;
    public bool IsGameOver()
    {
        return GameOver;
    }
    public bool IsGameRunning()
    {
        return GameRunning;
    }
    public bool IsLevelCompleted()
    {
        return LevelComplete;
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
    protected bool Suspended = false;
    public bool IsGameSuspended()
    {
        return Suspended;
    }
    public string GetLevelName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
