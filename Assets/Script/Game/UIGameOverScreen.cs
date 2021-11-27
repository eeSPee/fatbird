using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOverScreen : MonoBehaviour
{
    GameObject HighScoreAlert;
    Text LastScoreCount;
    Text HighScoreCount;
    private void Awake()
    {
        HighScoreAlert =  transform.Find("New High Score").gameObject;
        LastScoreCount = transform.Find("Yourscore Counter").GetComponent<Text>();
        HighScoreCount = transform.Find("Hiscore Counter").GetComponent<Text>();
    }
    public string GetLevelName()
    {
        return SceneManager.GetActiveScene().name; 
    }
    private void OnEnable()
    {
        float MyScore = ScoreController.main.Score;
        float OldScore = PlayerPrefs.GetFloat(GetLevelName() + " HighScore");
        LastScoreCount.text = MyScore + "";
        HighScoreCount.text = Mathf.Max(OldScore, MyScore) + "";
        
            HighScoreAlert.SetActive(MyScore > OldScore);
        

        SaveScores(MyScore, Mathf.Max(OldScore, MyScore));
    }

    public void SaveScores(float s, float hs)
    {
        string LevelName = GetLevelName();
        PlayerPrefs.SetFloat(GetLevelName() + " LastScore",s);
        PlayerPrefs.SetFloat(GetLevelName() + " HighScore",hs);
    }
}
