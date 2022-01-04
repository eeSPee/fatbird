using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostGameTimeCounter : MonoBehaviour
{
    bool awoken = false;
    Text text;
    private void Start()
    {
        text = GetComponent<Text>();
        awoken = true;
        UpdateScore();
    }
    private void OnEnable()
    {
        if (!awoken)
            return;
        UpdateScore();
    }
    public virtual void UpdateScore()
    {
        LevelController_Platformer lc = LevelController.main as LevelController_Platformer;
        if (PlayerPrefs.GetInt(LevelController.main.GetLevelName() + " Complete")==0)
        {
            text.text = "--M --S";
            return;
        }


            float num = PlayerPrefs.GetFloat(LevelController.main.GetLevelName() + " TimeCount");

        int mins = Mathf.FloorToInt(num/60);
        int secs = Mathf.FloorToInt(num-mins*60);
        text.text = mins+"M "+secs+"S";
    }
}
