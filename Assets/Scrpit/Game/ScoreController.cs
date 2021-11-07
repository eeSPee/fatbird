using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int ScorePerSecond = 100;
    public int Combo = 0;
    public int Score = 0;
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
    public IEnumerator RecordGameCoroutine()
    {
        Score = 0;
        Combo = 0;
        UIController.main.UpdateScore();
        score_start:
        {
            yield return new WaitForSeconds(1);
            Score += ScorePerSecond;
            UIController.main.UpdateScore();
            goto score_start;
        }
    }
    public void ScorePoints(int points, bool combo)
    {
        if (combo)
            Combo++;
        Score += points * Combo;
        UIController.main.UpdateScore();
    }
    public void ResetCombo()
    {
        Combo = 0;
        UIController.main.UpdateScore();
    }
}
    