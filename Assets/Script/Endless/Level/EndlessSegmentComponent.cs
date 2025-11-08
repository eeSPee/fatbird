using UnityEngine;

public class EndlessSegmentComponent : MonoBehaviour
{
    public WeightLevelSegment assignedSegment;
    public float segmentHeight = 10;
    public GameObject leftSide, rightSide;
    public float requiredWidth = 0;
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(100, segmentHeight, 0));
    }
}
