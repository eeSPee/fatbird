using UnityEngine;

public class FlapWingButton : MonoBehaviour
{
    public enum Wing {  left, right }
    public Wing wingControlled;
    public void OnButtonPress()
    {
        if (PlayerController.main!=null)
        {
            PlayerController.main.FlapWing(wingControlled == Wing.right);
        }
    }
}
