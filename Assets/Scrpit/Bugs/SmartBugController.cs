using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartBugController : BugController
{
    public float SightRange = 2f;
    public float AweTime = 12f;


    public override void OnSpawn()
    {
        AiState = BugState.wander;
        base.OnSpawn();
    }
    public override void OnMovement()
    {
        Vector3 orbit = HandleOrbit();
        switch (AiState)
        {
            case BugState.wander:
                transform.position += (Vector3.right * (moveRight ? 1 : -1) * Speed + orbit) * Time.deltaTime;
                break;
            case BugState.fleeing:
                transform.position -= (Vector3.right * (moveRight ? 1 : -1) * Speed + orbit)  * Time.deltaTime * 1.25f;
                break;
        }
    }
    enum BugState
    {
        wander,
        curious,
        fleeing
    }
    BugState AiState = BugState.wander;
    public override void OnAI()
    {
        switch (AiState)
        {
            case BugState.wander:
                if ((FatBirdController.main.transform.position - transform.position).sqrMagnitude < SightRange * SightRange)
                {
                    AiState = BugState.curious;
                    LastAiThinkTime = Time.time + AweTime;
                }
                break;
            case BugState.curious:
                AiState = BugState.fleeing;
                break;
            case BugState.fleeing:
                if (IsOutsideGameBounds())
                {
                    ScoreController.main.ResetCombo();
                    Kill(false);
                }
                break;
        }
    }
}
