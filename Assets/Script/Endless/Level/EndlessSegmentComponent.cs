using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSegmentComponent : MonoBehaviour
{
    public WeightLevelSegment assignedSegment;
    public float segmentHeight = 10;
    public GameObject leftSide, rightSide;
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(100, segmentHeight, 0));
    }
}
