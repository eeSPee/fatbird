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
        if (Input.GetKeyUp(KeyCode.R))
        {
            ResetGame();
        }
    }
    protected virtual void Start()
    {
    }
    public virtual void StartTheGame()
    {
        UIController.main.DisableTutorial();
        GameRunning = true;
        StartTime = Time.time;
    }
    public virtual void EndTheGame(bool victory)
    {
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
}
