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
        text.text = PlayerPrefs.GetInt(LevelController.main.GetLevelName() + " BugCount") + " / " + lc.bugs.Count;
    }
}
