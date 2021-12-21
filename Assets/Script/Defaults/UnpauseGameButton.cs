using UnityEngine;
using UnityEngine.UI;

public class UnpauseGameButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            LevelController.main.PauseUnpause(false);
        });
    }

}
