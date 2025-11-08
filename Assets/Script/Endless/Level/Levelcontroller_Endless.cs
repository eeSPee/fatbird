

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Levelcontroller_Endless : LevelController
{
    public ObjectPool segmentPool;
    public ObjectPool powerupPool;
    public float dividerHeight = 3;
    List<EndlessSegmentComponent> activeSegments = new();
    public WeightLevelSegment[] validSegments = new WeightLevelSegment[0];

    public float height = 0;
    public override void StartTheGame()
    {
        base.StartTheGame();
        ClearAllSegments();
        height = CameraController.main.camera.orthographicSize * 2;
        enabled = true;
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
                SpawnSegment(WeightList.PickWeight(validSegments.Where((s) => s.segmentHeight <= height)));
            }
    }
    void DespawnUnusedSegments()
    {
        float cameraUnder = CameraController.main.transform.position.y - CameraController.main.camera.orthographicSize;

        foreach (var segment in activeSegments.ToArray())
        {
            if (segment.transform.position .y + segment.segmentHeight / 2f < cameraUnder)
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
            if (pooled.TryGetComponent(out EndlessSegmentComponent seg) )
            {
                pooled.transform.position = new Vector3(0, height + seg.segmentHeight / 2f, 0);
                height += seg.segmentHeight + dividerHeight * (Random.value  + 1)/ 2;

                activeSegments.Add(seg);
                seg.assignedSegment = segmentData;

                if (seg.leftSide != null)
                    seg.leftSide.transform.localPosition = Mathf.Max(CameraController.main.camera.orthographicSize * CameraController.main.camera.aspect, seg.requiredWidth) * Vector3.left;
                if (seg.rightSide != null)
                    seg.rightSide.transform.localPosition = Mathf.Max(CameraController.main.camera.orthographicSize * CameraController.main.camera.aspect, seg.requiredWidth) * Vector3.right;
            }
            foreach (var spawn in pooled.GetComponentsInChildren<EndlessSegmentSpawnpoint>()) {
           //     spawn
            }
        }
    }
    void DespawnSegment(EndlessSegmentComponent seg)
    {
        segmentPool.DeactivateObject(seg.gameObject);
        activeSegments.Remove(seg);
    }
    void ClearAllSegments()
    {
        foreach (var segment in activeSegments.ToArray())
        {
                DespawnSegment(segment);
        }
        activeSegments.Clear();
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