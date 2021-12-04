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
}
