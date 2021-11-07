using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController main;
    GameObject[] Walls;
    private void Awake()
    {
        main = this;
        Walls = new GameObject[]
        {
            transform.Find("Level Bounds R").gameObject,
            transform.Find("Level Bounds L").gameObject,
        };
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ResetGame();
        }
    }

    public void StartTheGame()
    {
        UIController.main.DisableTutorial();
        ScoreController.main.StartRecording();
        BugPoolController.main.StartSpawning();
        Walls[0].transform.position = Vector3.right * (Camera.main.orthographicSize * Camera.main.aspect + .5f);
        Walls[1].transform.position = Vector3.left * (Camera.main.orthographicSize * Camera.main.aspect + .5f);
    }
    public void EndTheGame()
    {
        ScoreController.main.StopRecording();
        BugPoolController.main.StopSpawning();
        GameOver = true;
    }
    public void ResetGame()
    {
        EndTheGame();
        GameOver = false;
        BugPoolController.main.ClearBugs();
        FatBirdController.main.Reset();
    }
    bool GameOver = false;
    public bool IsGameOver()
    {
        return GameOver;
    }
}
