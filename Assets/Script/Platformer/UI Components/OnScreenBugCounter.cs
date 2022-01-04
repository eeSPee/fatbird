using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenBugCounter : MonoBehaviour
{
    int Score = 0;
    UnityEngine.UI.Text textC;
    public static OnScreenBugCounter main;
    void Awake()
    {
        main = this;
        textC = GetComponent<UnityEngine.UI.Text>();
    }
    public void SetEnabled(bool value)
    {
        transform.parent.gameObject.SetActive(value);

        if (value)
            ResetBugs();
    }
    void UpdateText()
    {
        LevelController_Platformer lc = LevelController.main as LevelController_Platformer;
        textC.text = Score + " / " + lc.bugs.Count;
    }
    public void IncreaseScore()
    {
        Score++;
        UpdateText();
    }
    public void ResetBugs()
    {

        Score = ((LevelController_Platformer)LevelController.main).RecountBugsCollected();// PlayerPrefs.GetInt(LevelController.main.GetLevelName() + " BugCount");
        UpdateText();
    }
}
