using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOverScreen : MonoBehaviour
{
    public string GetLevelName()
    {
        return SceneManager.GetActiveScene().name; 
    }
    private void OnEnable()
    {
        float MyScore = ScoreController.main.Score;
        float OldScore = PlayerPrefs.GetFloat(GetLevelName() + " HighScore");
        transform.Find("Yourscore Counter").GetComponent<Text>().text = MyScore + "";
        transform.Find("Hiscore Counter").GetComponent<Text>().text = Mathf.Max(OldScore, MyScore) + "";
        if (MyScore > OldScore)
        {
            transform.Find("New High Score").gameObject.SetActive(true);
            
        }

        SaveScores(MyScore, Mathf.Max(OldScore, MyScore));
    }

    public void SaveScores(float s, float hs)
    {
        string LevelName = GetLevelName();
        PlayerPrefs.SetFloat(GetLevelName() + " LastScore",s);
        PlayerPrefs.SetFloat(GetLevelName() + " HighScore",hs);
    }
}
