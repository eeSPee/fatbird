using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeObstacleController : MonoBehaviour
{
    Collider2D mcollider;
    Vector3 startPosition;
    public Vector3 start = Vector3.zero;
    public float WaitForScore = 0;
    public float ComeInTime = 3;

    private void Awake()
    {
        startPosition = transform.localPosition;
        mcollider = GetComponent<Collider2D>();
    }

    protected Coroutine spawnCoroutine;
    public void StartSpawning()
    {
        Reset();
        spawnCoroutine = StartCoroutine(SpawnIntoGame());
    }
    protected virtual IEnumerator SpawnIntoGame()
    {
        yield return new WaitWhile (() => { return ScoreController.main.Score < WaitForScore; });

        float warningFrames = ComeInTime * 60;
        float retractFrames = ComeInTime * 60;
        float attackFrames = ComeInTime * 20;

        mcollider.enabled = false;
        for (int I = 0; I < warningFrames; I++)
        {
            transform.localPosition -= start / warningFrames;
            yield return new WaitForEndOfFrame();
        }
        for (int I = 0; I < retractFrames; I++)
        {
            transform.localPosition += start / retractFrames;
            yield return new WaitForEndOfFrame();
        }
        mcollider.enabled = true;
        for (int I = 0; I < attackFrames; I++)
        {
            transform.localPosition -= start / attackFrames;
            yield return new WaitForEndOfFrame();
        }
    }
    public virtual void Reset()
    {
        if (spawnCoroutine!=null)
        {
            StopCoroutine(spawnCoroutine);
        }
        transform.localPosition = startPosition + start;
    }
}