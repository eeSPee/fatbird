using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBug : BugController
{
    bool moveRight = false;
    public override void OnSpawn()
    {
        LastAiThinkTime = Time.time + 2f / Speed;
        moveRight = Random.value < .5f;
        transform.position = new Vector3(
            -(Camera.main.aspect * Camera.main.orthographicSize + 1) * (moveRight ? 1 : -1),
            Random.value * 6 - 2,
            1
           );
    }
    public override void OnMovement()
    {
        transform.position += (Vector3.right * (moveRight ? 1 : -1) * Speed + HandleOrbit()) * Time.deltaTime;
    }
    public override void OnAI()
    {
        if (IsOutsideGameBounds())
        {
            ScoreController.main.ResetCombo();
            Kill(false);
        }
    }
}
