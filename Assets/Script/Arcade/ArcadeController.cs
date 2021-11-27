using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeController : LevelController
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
        Walls[0].transform.position = Vector3.right * (Camera.main.orthographicSize * Camera.main.aspect + .5f);
        Walls[1].transform.position = Vector3.left * (Camera.main.orthographicSize * Camera.main.aspect + .5f);
        Walls[2].transform.localScale = new Vector3((Camera.main.orthographicSize * Camera.main.aspect) * 2, 1, 1);
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
    public override void ResetGame()
    {
        base.ResetGame();
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
