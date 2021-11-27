using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    public string ToGoScreen = "Arcade";
  public void OnPlayPressed()
    {
        SceneManager.LoadScene(ToGoScreen);
    }
}
