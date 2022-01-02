
using UnityEngine;
using UnityEngine.UI;

public class ResetGameButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            LevelController.main.ResetGame(LevelController.main.IsLevelCompleted());
        });
    }

}
