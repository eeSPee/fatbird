using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Platformer : PlayerController
{
    CheckPointController lastCheckPoint;
    public AudioClip AudioClipNest;
    public AudioClip AudioClipVictory;
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
        }
            if (collision.gameObject.tag == "Victory" && !LevelController.main.IsGameOver())
        {
            LevelController.main.EndTheGame(true);
            PlayerController.main.AudioSource.PlayOneShot(AudioClipVictory);
            PlayerPrefs.SetInt(LevelController.main.GetLevelName() + " Complete", 1);
            if (collision.gameObject.TryGetComponent<CheckPointController>(out CheckPointController chp))
                chp.SetActivated(true);
        }
    }
    public void SetCheckPoint(CheckPointController checkpoint)
    {
        if (lastCheckPoint == checkpoint)
            return;
        lastCheckPoint = checkpoint;
        checkpoint.SetActivated(true);
        start = lastCheckPoint.transform.position;
        string varName = LevelController.main.GetLevelName() + " CheckpointProgress";
        PlayerPrefs.SetInt(varName, Mathf.Max(PlayerPrefs.GetInt(varName), checkpoint.CheckPointID));
        PlayerPrefs.SetInt(LevelController.main.GetLevelName() + " LastCheckPoint", checkpoint.CheckPointID);
        if (enabled)
        {
            SpecialEffectPooler.main.CreateSpecialEffect("BugPickup", transform.position);
            SpecialEffectPooler.main.TextEffect("CHECKPOINT!", transform.position + Vector3.up * .33f);
            PlayerController.main.AudioSource.PlayOneShot(AudioClipNest);
        }
    }
}
