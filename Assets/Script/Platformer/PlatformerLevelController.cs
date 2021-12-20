using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerLevelController : LevelController
{
    bool timerCountdown = false;
    public List<PlatformerBugController> bugs;
    protected override void Awake()
    {
        base.Awake();
        bugs = new List<PlatformerBugController>();
        checkPoints = new List<CheckPointController>();
        checkPoints.AddRange(Object.FindObjectsOfType<CheckPointController>());
        bugs = new List<PlatformerBugController>();
        bugs.AddRange(Object.FindObjectsOfType<PlatformerBugController>());
    }
    public override void PlayerEnterSafezone()
    {
        int bugsCollected = 0;
        foreach (PlatformerBugController bug in bugs)
        {
            if (bug.wasCollected)
            {
                bug.RemoveFromTheGame();
                bugsCollected++;
            }
        }
        PlayerPrefs.SetInt(LevelController.main.GetLevelName() + " BugCount", bugsCollected);
    }
    public override void StartTheGame()
    {
        base.StartTheGame();

    }
    public override void ResetGame()
    {
        foreach (PlatformerBugController bug in bugs)
        {
            if (!bug.wasRemoved)
            {
                bug.gameObject.SetActive(true);
            }
        }
        base.ResetGame();
    }
    public List<CheckPointController> checkPoints;
    public CheckPointController GetCheckpointByID(int ID)
    {
        foreach (CheckPointController ch in checkPoints)
        {
            if (ch.CheckPointID == ID)
                return ch;
        }
        return null;
    }
}
