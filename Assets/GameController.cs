using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController main;
    GameObject[] Walls;
    List<SpikeController> Spikes = new List<SpikeController>();
    private void Awake()
    {
        main = this;
        Walls = new GameObject[]
        {
            transform.Find("Level Bounds R").gameObject,
            transform.Find("Level Bounds L").gameObject,
            transform.Find("Level Bounds T").gameObject,
        };
        Spikes.AddRange(gameObject.GetComponentsInChildren<SpikeController>());
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ResetGame();
        }
    }
    public void Start()
    {
        Walls[0].transform.position = Vector3.right * (Camera.main.orthographicSize * Camera.main.aspect + .5f);
        Walls[1].transform.position = Vector3.left * (Camera.main.orthographicSize * Camera.main.aspect + .5f);
        Walls[2].transform.localScale = new Vector3((Camera.main.orthographicSize * Camera.main.aspect) * 2, 1, 1);
        ResetSpikes();
    }
    public void StartTheGame()
    {
        UIController.main.DisableTutorial();
        ScoreController.main.StartRecording();
        BugPoolController.main.StartSpawning();
        ArmSpikes();
    }
    public void EndTheGame()
    {
        ScoreController.main.StopRecording();
        BugPoolController.main.StopSpawning();
        ScoreController.main.ResetCombo();
        UIController.main.EnableGameOverScreen();
        GameOver = true;
    }
    public void ResetGame()
    {
        EndTheGame();
        GameOver = false;
        BugPoolController.main.ClearBugs();
        FatBirdController.main.Reset();
        UIController.main.DisableGameOverScreen();
        ScoreController.main.Score = 0;
        ResetSpikes();
    }
    bool GameOver = false;
    public bool IsGameOver()
    {
        return GameOver;
    }
    public void ArmSpikes()
    {
        foreach (SpikeController spike in Spikes)
        {
            spike.StartSpawning();
        }
    }
    public void ResetSpikes()
    {
        foreach (SpikeController spike in Spikes)
        {
            spike.Reset();
        }
    }
}
