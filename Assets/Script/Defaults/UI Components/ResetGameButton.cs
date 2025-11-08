
using UnityEngine;
using UnityEngine.UI;

public class ResetGameButton : MonoBehaviour
{
    public void Press()
    {
            LevelController.main.ResetGame(LevelController.main.IsLevelCompleted());
    }

}
