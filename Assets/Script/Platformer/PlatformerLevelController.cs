using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerLevelController : LevelController
{
    public float GameTime = 10;

    public override float GetGameTime()
    {
        return GameTime - base.GetGameTime();
    }
    /*protected override void Update()
    {
        base.Update();
        if (!IsGameOver() && !IsGameSuspended() && GetGameTime()<=0)
            {
            EndTheGame(false);
        }
    }*/
    public List<PlatformerBugController> bugs;
    protected override void Awake()
    {
        base.Awake();
        bugs = new List<PlatformerBugController>();
    }
    public override void SuspendGame()
    {
        foreach (PlatformerBugController bug in bugs)
        {
            if (bug.gameObject.activeInHierarchy)
            {
                bug.RemoveFromTheGame();
            }
        }
    }
    public override void ResetGame()
    {
        base.ResetGame();
        foreach (PlatformerBugController bug in bugs)
        {
            if (!bug.wasRemoved)
            {
                bug.gameObject.SetActive(true);
            }
        }
    }
}
