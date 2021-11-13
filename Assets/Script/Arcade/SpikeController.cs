using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    Vector3 startPosition;
    public Vector3 start = Vector3.zero;
    public float WaitForScore = 0;
    public float ComeInTime = 3;

    private void Awake()
    {
        startPosition = transform.localPosition;
    }

    Coroutine spawnCoroutine;
    public void StartSpawning()
    {
        Reset();
        spawnCoroutine = StartCoroutine(SpawnIntoGame());
    }
    IEnumerator SpawnIntoGame()
    {
        yield return new WaitWhile (() => { return ScoreController.main.Score < WaitForScore; });

        float warningFrames = ComeInTime * 60;
        float retractFrames = ComeInTime * 60;
        float attackFrames = ComeInTime * 20;

        for (int I = 0; I < warningFrames; I++)
        {
            transform.localPosition -= start / warningFrames * .8f;
            yield return new WaitForEndOfFrame();
        }
        for (int I = 0; I < retractFrames; I++)
        {
            transform.localPosition += start / retractFrames * .8f;
            yield return new WaitForEndOfFrame();
        }
        for (int I = 0; I < attackFrames; I++)
        {
            transform.localPosition -= start / attackFrames;
            yield return new WaitForEndOfFrame();
        }
    }
    public void Reset()
    {
        if (spawnCoroutine!=null)
        {
            StopCoroutine(spawnCoroutine);
        }
        transform.localPosition = startPosition + start;
    }
}
