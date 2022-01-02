using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BugCounterPlatformerUI : MonoBehaviour
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
        PlatformerLevelController lc = LevelController.main as PlatformerLevelController;
        float bugCount = ((PlatformerLevelController)LevelController.main).RecountBugsCollected();// PlayerPrefs.GetInt(LevelController.main.GetLevelName() + " BugCount");
        text.text = bugCount + " / " + lc.bugs.Count;
    }
}
