using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenTimer : MonoBehaviour
{
    UnityEngine.UI.Text textC;
    private void Awake()
    {
        textC = GetComponent<UnityEngine.UI.Text>();
    }
    public void Update()
    {
            float gameTime = LevelController.main.GetGameTime();
            float mins = Mathf.FloorToInt(gameTime / 60);
            float secs = Mathf.FloorToInt(gameTime - mins * 60);

        textC.text = mins + ":" + (secs < 10 ? "0" : "") + secs;
        
    }
}
