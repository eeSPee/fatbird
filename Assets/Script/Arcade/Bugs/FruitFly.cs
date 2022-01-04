using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitFly : ScoreBugController
{
    public override void OnSpawn()
    {
        LastAiThinkTime = Time.time + 2f / Speed;
        base.OnSpawn();
    }
    public override void OnMovement()
    {
        transform.position += (Vector3.right * (moveRight ? 1 : -1) * Speed + HandleOrbit()) * Time.deltaTime;
    }
    public override void OnAI()
    {
        if (IsOutsideGameBounds())
        {
            Kill(false);
        }
    }
}
