using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdPlatformer : FatBirdController
{
   public static int StartingCheckPoint = 0;
    CheckPointController lastCheckPoint;
    public AudioClip AudioClipNest;
    public AudioClip AudioClipVictory;
    protected AudioSource audioSource_fatbird;

    private void Start()
    {
      audioSource_fatbird = FatBirdController.main.AudioSource;
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
            SetCheckPoint(collision.gameObject.GetComponent<CheckPointController>());
            LevelController.main.PlayerEnterSafezone();
        }
            if (collision.gameObject.tag == "Victory" && !LevelController.main.IsGameOver())
        {
            LevelController.main.EndTheGame(true);
            audioSource_fatbird.PlayOneShot(AudioClipVictory);
        }
    }
    public void SetCheckPoint(CheckPointController checkpoint)
    {
        if (lastCheckPoint == checkpoint)
            return;
        lastCheckPoint = checkpoint;
        checkpoint.SetEmpty(true);
        start = lastCheckPoint.transform.position;
        string varName = LevelController.main.GetLevelName() + " CheckpointProgress";
        PlayerPrefs.SetInt(varName, Mathf.Max(PlayerPrefs.GetInt(varName), checkpoint.CheckPointID));
        if (enabled)
        {
            SpecialEffectPooler.main.CreateSpecialEffect("BugPickup", transform.position);
            SpecialEffectPooler.main.TextEffect("CHECKPOINT!", transform.position + Vector3.up * .33f);
            audioSource_fatbird.PlayOneShot(AudioClipNest);
        }
    }
}
