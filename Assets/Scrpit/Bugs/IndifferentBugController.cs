using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndifferentBugController : BugController
{
    bool moveRight = false;
    public float StopDistance = 5;
    public float HoverTime = 15;
    public override void OnSpawn()
    {
        moveRight = Random.value < .5f;
        transform.position = new Vector3(
            -(Camera.main.aspect * Camera.main.orthographicSize + 1) * (moveRight ? 1 : -1),
            Random.value * 6 - 2,
            1
           );

        AiState = BugState.comein;
    }
    public override void OnMovement()
    {
        switch (AiState)
        {
            case BugState.comein:
                transform.position += (Vector3.right * (moveRight ? 1 : -1) * Speed + HandleOrbit()) * Time.deltaTime;
                break;
            case BugState.hover:
                transform.position += HandleOrbit() * Time.deltaTime;
                break;
            case BugState.fleeing:
                transform.position -= (Vector3.right * (moveRight ? 1 : -1) * Speed + HandleOrbit()) * Time.deltaTime;
                break;
        }
    }
    public override Vector3 HandleOrbit()
    {
        float timedelta = (Time.time + Variation) % 2 * Mathf.PI;
        return new Vector2(Mathf.Sin(timedelta), Mathf.Cos(timedelta)) * OrbitRange;
    }
    enum BugState
    {
        comein,
        hover,
        fleeing
    }
    BugState AiState = BugState.comein;
    public override void OnAI()
    {
        switch (AiState)
        {
            case BugState.comein:
                if (Mathf.Abs(transform.position.x) < Camera.main.orthographicSize * Camera.main.aspect - StopDistance)
                {
                    AiState = BugState.hover;
                    LastAiThinkTime = Time.time + HoverTime;
                }
                break;
            case BugState.hover:
                AiState = BugState.fleeing;
                break;
            case BugState.fleeing:
                if (IsOutsideGameBounds())
                {
                    Kill(false);
                }
                break;
        }
    }
}
