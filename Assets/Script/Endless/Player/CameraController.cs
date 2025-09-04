using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController main;
    private void Awake()
    {
        main = this;
    }
    public Camera camera;

    public GameObject leftWall, rightWall;
    protected void Update()
    {
        AdjustWalls();
    }
    void AdjustWalls()
    {
        if (leftWall != null)
            leftWall.transform.position = camera.transform.position + Vector3.right * (camera.orthographicSize * camera.aspect + .5f);
        if (rightWall != null)
            rightWall.transform.position = camera.transform.position + Vector3.left * (camera.orthographicSize * camera.aspect + .5f);
    }
}
