using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdPlatformer : FatBirdController
{
    int StartingCheckPoint = 0;
    CheckPointController lastCheckPoint;
    public AudioSource AudioSourceCheckpoint;

    public override void Update()
    {
        base.Update();
        Camera.main.transform.position = transform.position + Vector3.back*10;
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.tag == "Checkpoint" && !LevelController.main.IsGameOver())
        {
            SetCheckPoint(collision.gameObject.GetComponent<CheckPointController>());
            LevelController.main.SuspendGame();
        }
            if (collision.gameObject.tag == "Victory" && !LevelController.main.IsGameOver())
        {
            //TODO shertigan add a victory sound?
            LevelController.main.EndTheGame(true);
            AudioSourceCheckpoint.Play();
        }
    }
    public void SetCheckPoint(CheckPointController checkpoint)
    {
        if (lastCheckPoint == checkpoint)
            return;
        lastCheckPoint = checkpoint;
        start = lastCheckPoint.transform.position + Vector3.up;
        SpecialEffectPooler.main.CreateSpecialEffect("BugPickup", transform.position);
        SpecialEffectPooler.main.TextEffect("CHECKPOINT!", transform.position + Vector3.up * .33f);
        AudioSourceCheckpoint.Play();
    }
}
