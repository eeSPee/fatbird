using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public float Score = 0;
    public int ScorePerSecond = 100;
    public int Combo = 0;
    public float ComboResetTime = 10;
    public static ScoreController main;
    private void Awake()
    {
        main = this;
    }


    Coroutine scoreCoroutine;
    public void StartRecording()
    {
        StopRecording();
        scoreCoroutine = StartCoroutine(RecordGameCoroutine());
    }
    public void StopRecording()
    {
        if (IsScoring())
            StopCoroutine(scoreCoroutine);
        scoreCoroutine = null;
    }
    public bool IsScoring()
    {
        return (scoreCoroutine != null);
    }
    float comboResetTime = 0;
    public IEnumerator RecordGameCoroutine()
    {
        Score = 0;
        Combo = 0;
        score_start:
        {
            if (Combo > 0 && comboResetTime < Time.time)
            {
                ResetCombo();
            }
            yield return new WaitForSeconds(.1f);
            if (!FatBirdController.main.IsGrounded())
                Score += ScorePerSecond * .1f;
            goto score_start;
        }
    }
    public void ScorePoints(int points, bool combo)
    {
        if (combo)

        {
            transform.Find("ComboParticle").gameObject.SetActive(true);
            Combo++;
            comboResetTime = Time.time + ComboResetTime;
        }
            Score += points * Combo;
    }
    public void ResetCombo()
    {
        Combo = 0;
    }
}
    