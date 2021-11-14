using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdPlatformer : FatBirdController
{
    public override void Update()
    {
        base.Update();
        Camera.main.transform.position = transform.position + Vector3.back*10;
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.tag == "Victory" && !LevelController.main.IsGameOver())
        {
            //TODO shertigan add a victory sound?
            LevelController.main.EndTheGame(true);
        }
    }
}
