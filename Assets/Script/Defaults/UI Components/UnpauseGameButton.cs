using UnityEngine;
using UnityEngine.UI;

public class UnpauseGameButton : MonoBehaviour
{
    public void Press()
    {
            LevelController.main.PauseUnpause(false);
    }

}
