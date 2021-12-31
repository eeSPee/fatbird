using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelSelectScores : MonoBehaviour
{
    public string LevelName = "Arcade";
    Text LevelText;
    Text HighScoreText;
    Text LastScoreText;

    private void Awake()
    {
        LevelText = transform.Find("Level Label")?.GetComponentInChildren<Text>();
        HighScoreText = transform.Find("High Score Label")?.Find("Score")?.GetComponent<Text>();
        LastScoreText = transform.Find("Last Score Label")?.Find("Score")?.GetComponent<Text>();
    }
    void Start()
    {
        if (LevelText!=null)
            LevelText.text = LevelName;
    }

    void OnEnable()
    {

        if (HighScoreText!=null)
        HighScoreText.text = PlayerPrefs.GetFloat(LevelName + " HighScore")+"";
        if (LastScoreText != null)
            LastScoreText.text = PlayerPrefs.GetFloat(LevelName + " LastScore") + "";
    }
    public void OnPlayPressed()
    {
        SceneManager.LoadSceneAsync(LevelName);
    }
}
