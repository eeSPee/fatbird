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
        if (Input.GetKeyUp(KeyCode.Escape))
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
    }
    public virtual void EndTheGame()
    {
        UIController.main.EnableGameOverScreen();
        GameOver = true;
        GameRunning = false;
    }
    public virtual void ResetGame()
    {
        EndTheGame();
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
}
