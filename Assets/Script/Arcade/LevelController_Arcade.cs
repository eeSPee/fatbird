using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController_Arcade : LevelController
{
    GameObject[] Walls;
    List<IArcadeObstacle> Spikes = new List<IArcadeObstacle>();
    protected override void Awake()
    {
        base.Awake();
        Walls = new GameObject[]
        {
            transform.Find("Level Bounds R").gameObject,
            transform.Find("Level Bounds L").gameObject,
            transform.Find("Level Bounds T").gameObject,
        };
        Spikes.AddRange(gameObject.GetComponentsInChildren<IArcadeObstacle>());
    }
    protected override void Start()
    {
        float ScreenScale = Camera.main.orthographicSize * Camera.main.aspect;
        Walls[0].transform.position = Vector3.right * (ScreenScale + .5f);
        Walls[1].transform.position = Vector3.left * (ScreenScale + .5f);
        Walls[2].transform.localScale = new Vector3(ScreenScale * 2, 1, 1);
        ResetSpikes();
    }
    public override void StartTheGame()
    {
        ScoreController.main.StartRecording();
        BugPoolController.main.StartSpawning();
        ArmSpikes();
        base.StartTheGame();
    }
    public override void EndTheGame(bool victory)
    {
        ScoreController.main.StopRecording();
        BugPoolController.main.StopSpawning();
        ScoreController.main.ResetCombo();
        base.EndTheGame(victory);
    }
    public override void ResetGame(bool hardReset)
    {
        base.ResetGame(hardReset);
        BugPoolController.main.ClearBugs();
        ScoreController.main.Score = 0;
        ResetSpikes();
    }
    public void ArmSpikes()
    {
        foreach (IArcadeObstacle spike in Spikes)
        {
            spike.StartSpawning();
        }
    }
    public void ResetSpikes()
    {
        foreach (IArcadeObstacle spike in Spikes)
        {
            spike.Reset();
        }
    }
}
