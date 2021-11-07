using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    Vector3 startPosition;
    public Vector3 start = Vector3.zero;
    public float startDelay = 0;

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
        yield return new WaitForSeconds(startDelay);
        int nComeInFrames = 90;
        for (int I = 0; I< nComeInFrames; I ++ )
        {
            transform.localPosition -= start / nComeInFrames;
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
