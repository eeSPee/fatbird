using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
  public void OnPlayPressed()
    {
        SceneManager.LoadScene("Arcade");
    }
}
