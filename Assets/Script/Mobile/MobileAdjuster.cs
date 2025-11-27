    using UnityEngine;

public class MobileAdjuster : MonoBehaviour
{
    public float mobileCameraOsize = 12;
    public float pcCameraOsize = 12;
    public GameObject[] mobileGameObjects = new GameObject[0];
    public GameObject[] pcGameObjects = new GameObject[0];
    bool IsOnMobile()
    {
        return Screen.width / Screen.height < 1;
    }
    private void Awake()
    {
        ToggleMobileMode(IsOnMobile());
    }
    void ToggleMobileMode(bool value)
    {
        if (CameraController.main!= null)
        {
            CameraController.main.camera.orthographicSize = value ? mobileCameraOsize : pcCameraOsize;  
        }
        if (mobileGameObjects!=null)
        {
            foreach (GameObject gameObject in mobileGameObjects)
            {
                gameObject?.SetActive(value);
            }
        }
        if (pcGameObjects != null)
        {
            foreach (GameObject gameObject in pcGameObjects)
            {
                gameObject?.SetActive(!value);
            }
        }
    }
}
