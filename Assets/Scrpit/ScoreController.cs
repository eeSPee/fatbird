using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int ScorePerSecond = 100;
    int Combo = 0;
    public int Score = 0;
    public static ScoreController main;
    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        StartRecording();
    }

    Coroutine scoreCoroutine;
    public void StartRecording()
    {
        StopRecording();
        scoreCoroutine = StartCoroutine(RecordGameCoroutine());
    }
    public void StopRecording()
    {
        if (scoreCoroutine!=null)
            StopCoroutine(scoreCoroutine);
    }
    public IEnumerator RecordGameCoroutine()
    {
        score_start:
        {
            yield return new WaitForSeconds(1);
            Score += ScorePerSecond;
            goto score_start;
        }
    }
    public void ScorePoints(int points, bool combo)
    {
        Score += points * Combo;
        if (combo)
            Combo++;
    }
    public void ResetCombo()
    {
        Combo = 0;
    }
}
    