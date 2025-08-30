
using System;
using UnityEngine;

public class Levelcontroller_Endless : LevelController
{
    public ObjectPool segmentPool;
    public WeightLevelSegment[] validSegments = new WeightLevelSegment[0];
}

[Serializable]
public class WeightLevelSegment : WeightList.WeightEntry
    {
    public GameObject segmentPrefab;
    public float minimumHeight = 10;

    public WeightLevelSegment(float weight, float minimumHeight = 0, GameObject segmentPrefab = null) : base(weight)
    {
        this.minimumHeight = minimumHeight;
        this.segmentPrefab = segmentPrefab;
    }
}