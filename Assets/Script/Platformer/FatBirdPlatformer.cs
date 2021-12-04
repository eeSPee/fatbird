using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdPlatformer : FatBirdController
{
    int StartingCheckPoint = 0;
    CheckPointController lastCheckPoint;
    private void Start()
    {
        SetCheckPoint( CheckPointController.GetCheckpointByID(StartingCheckPoint),false);
    }
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
            SetCheckPoint(collision.gameObject.GetComponent<CheckPointController>(),true);

        }
            if (collision.gameObject.tag == "Victory" && !LevelController.main.IsGameOver())
        {
            //TODO shertigan add a victory sound?
            LevelController.main.EndTheGame(true);
        }
    }
    public void SetCheckPoint(CheckPointController checkpoint, bool effect)
    {
        if (lastCheckPoint == checkpoint || checkpoint==null)
            return;
        lastCheckPoint = checkpoint;
        start = lastCheckPoint.transform.position + Vector3.up;

        if (effect)
        {
            SpecialEffectPooler.main.CreateSpecialEffect("BugPickup", transform.position);
            SpecialEffectPooler.main.TextEffect("CHECKPOINT!", transform.position + Vector3.up * .33f);
        }
    }
}
