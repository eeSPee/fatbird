

using System;
using System.Collections.Generic;
using UnityEngine;

public class Levelcontroller_Endless : LevelController
{
    public ObjectPool segmentPool;
    public ObjectPool powerupPool;
    List<EndlessSegmentComponent> activeSegments = new();
    public WeightLevelSegment[] validSegments = new WeightLevelSegment[0];

    public float height = 0;
    public override void StartTheGame()
    {
        base.StartTheGame();
        activeSegments.Clear();
        height = CameraController.main.camera.orthographicSize;
    }
    protected override void Update()
    {
        base.Update();
        TrySpawnNewSegments();
        DespawnUnusedSegments();
    }
    void TrySpawnNewSegments()
    {
        float cameraOver = CameraController.main.transform.position.y + CameraController.main.camera.orthographicSize;

        while (height < cameraOver)
            if (validSegments.Length < 1) return;
            else if (validSegments.Length == 1) {
                SpawnSegment(validSegments[0]);
                return;
            }
            else
            {
                SpawnSegment(WeightList.PickWeight(validSegments));
            }
    }
    void DespawnUnusedSegments()
    {
        float cameraUnder = CameraController.main.transform.position.y - CameraController.main.camera.orthographicSize;

        foreach (var segment in activeSegments.ToArray())
        {
            if (segment.transform.position .y + segment.assignedSegment.segmentHeight / 2f < cameraUnder)
            {
                DespawnSegment(segment);
            }
        }
    }
     void SpawnSegment(WeightLevelSegment segmentData)
    {
        GameObject pooled = segmentPool.PoolItem(segmentData.segmentPrefab);
        if (pooled != null)
        {
            pooled.transform.position = new Vector3(0, height + segmentData.segmentHeight / 2f, 0);
            height += segmentData.segmentHeight;

            if (pooled.TryGetComponent(out EndlessSegmentComponent seg) )
            {
                activeSegments.Add(seg);
                seg.assignedSegment = segmentData;
            }
            foreach (var spawn in pooled.GetComponentsInChildren<EndlessSegmentSpawnpoint>()) {
           //     spawn
            }
        }
    }
    void DespawnSegment(EndlessSegmentComponent seg)
    {

    }
}

[Serializable]
public class WeightLevelSegment : WeightList.WeightEntry
    {
    public GameObject segmentPrefab;
    public float segmentHeight = 10;

    public WeightLevelSegment(float weight, float minimumHeight = 0, GameObject segmentPrefab = null) : base(weight)
    {
        this.segmentHeight = minimumHeight;
        this.segmentPrefab = segmentPrefab;
    }
}