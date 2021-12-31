using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
   public void OnPressed()
    {
#if UNITY_EDITOR
            PlayerPrefs.DeleteAll();
#else
        Application.Quit();
#endif
    }
}
