using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController_Platformer : LevelController
{
    public List<BugCollectibleController> bugs;
    protected override void Awake()
    {
        base.Awake();
        bugs = new List<BugCollectibleController>();
        checkPoints = new List<CheckPointController>();
        checkPoints.AddRange(Object.FindObjectsOfType<CheckPointController>());
        bugs = new List<BugCollectibleController>();
        bugs.AddRange(Object.FindObjectsOfType<BugCollectibleController>());
    }
    protected override void Start()
    {
        base.Start();
        JumpToCheckpoint(0);
    }
    public int RecountBugsCollected()
    {
        int bugsCollected = 0;
        foreach (BugCollectibleController bug in bugs)
        {
            if (bug.WasCollected())
            {
             //   bug.RemoveFromTheGame();
                bugsCollected++;
            }
        }
        return bugsCollected;
    }
    public override void ResetGame(bool hardReset)
    {
        base.ResetGame(hardReset);
        if (hardReset)
        {
            JumpToCheckpoint(0);
            foreach (BugCollectibleController bug in bugs)
            {
                //if (!bug.wasRemoved)
                {
                    bug.gameObject.SetActive(true);
                }
            }
            foreach (CheckPointController ch in checkPoints)
            {
                ch.SetActivated(false);
            }
        }
        OnScreenBugCounter.main.ResetBugs();
    }
    public override void EndTheGame(bool victory)
    {
        if (victory)
        {
            if (PlayerPrefs.GetFloat(LevelController.main.GetLevelName() + " TimeCount")>0)
            PlayerPrefs.SetFloat(LevelController.main.GetLevelName() + " TimeCount", Mathf.Min(PlayerPrefs.GetFloat(LevelController.main.GetLevelName() + " TimeCount"), GetGameTime()));
            else
                PlayerPrefs.SetFloat(LevelController.main.GetLevelName() + " TimeCount", GetGameTime());
            PlayerPrefs.SetInt(LevelController.main.GetLevelName() + " BugCount", Mathf.Max(PlayerPrefs.GetInt(LevelController.main.GetLevelName()), RecountBugsCollected()));
        }
        base.EndTheGame(victory);
    }
    public void RecordTime()
    {
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
    public int currentCheckpoint = 0;
    public void JumpToCheckpoint(int nLevel)
    {
        int max = GetMaxCheckPoints();
        nLevel = Mathf.Clamp(nLevel, 0, max);

        CheckPointController checkpoint = (LevelController.main as LevelController_Platformer).GetCheckpointByID(nLevel);
        if (checkpoint != null)
        {
            currentCheckpoint = nLevel;
            Camera.main.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y, Camera.main.transform.position.z);
            PlayerController.main.transform.position = checkpoint.transform.position;
            (PlayerController.main as PlayerController_Platformer).SetCheckPoint(checkpoint);

        }
    }
    public int GetMaxCheckPoints()
    {
        return PlayerPrefs.GetInt(LevelController.main.GetLevelName() + " CheckpointProgress");
    }
}
