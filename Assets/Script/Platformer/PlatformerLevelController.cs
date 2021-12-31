using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerLevelController : LevelController
{
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
        base.PlayerEnterSafezone();
        RecountBugsCollected();
        PlayerPrefs.SetFloat(LevelController.main.GetLevelName() + " Checkpoint "+ PlayerPrefs.GetInt(LevelController.main.GetLevelName() + " LastCheckPoint") + " TimeCount", LevelController.main.GetGameTime());

        UIControllerPlatformer uic = UIController.main as UIControllerPlatformer;
    }
    public void RecountBugsCollected()
    {
        int bugsCollected = 0;
        foreach (PlatformerBugController bug in bugs)
        {
            if (bug.WasCollected())
            {
                bug.RemoveFromTheGame();
                bugsCollected++;
            }
        }
        PlayerPrefs.SetInt(LevelController.main.GetLevelName() + " BugCount", Mathf.Max(PlayerPrefs.GetInt(LevelController.main.GetLevelName()), bugsCollected));
    }
    /*public override void PlayerLeaveSafezone()
    {
        base.PlayerLeaveSafezone();
        UIControllerPlatformer uic = UIController.main as UIControllerPlatformer;
        uic.ShowTimer = true;
    }*/
    public override void StartTheGame()
    {
        base.StartTheGame();
        BugCounter.main.SetEnabled( true);
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
        BugCounter.main.ResetBugs();
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
