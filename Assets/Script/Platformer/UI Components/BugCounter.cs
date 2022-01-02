using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugCounter : MonoBehaviour
{
    int Score = 0;
    UnityEngine.UI.Text textC;
    public static BugCounter main;
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
        PlatformerLevelController lc = LevelController.main as PlatformerLevelController;
        textC.text = Score + " / " + lc.bugs.Count;
    }
    public void IncreaseScore()
    {
        Score++;
        UpdateText();
    }
    public void ResetBugs()
    {

        Score = ((PlatformerLevelController)LevelController.main).RecountBugsCollected();// PlayerPrefs.GetInt(LevelController.main.GetLevelName() + " BugCount");
        UpdateText();
    }
}
