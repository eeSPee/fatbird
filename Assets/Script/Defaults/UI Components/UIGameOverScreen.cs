using UnityEngine;
using UnityEngine.UI;

public class UIGameOverScreen : MonoBehaviour
{
    public GameObject HighScoreAlert;
    public Text LastScoreCount;
    public Text HighScoreCount;
  
    private void OnEnable()
    {
        float MyScore = ScoreController.main == null ? 0 : ScoreController.main.GetTotalScore();
        float OldScore = PlayerPrefs.GetFloat(LevelController.main.GetLevelName() + " HighScore");
        LastScoreCount.text = MyScore + "";
        HighScoreCount.text = Mathf.Max(OldScore, MyScore) + "";
        
            HighScoreAlert.SetActive(MyScore > OldScore);
        

        SaveScores(MyScore, Mathf.Max(OldScore, MyScore));
    }

    public void SaveScores(float s, float hs)
    {
        string LevelName = LevelController.main.GetLevelName();
        PlayerPrefs.SetFloat(LevelName + " LastScore",s);
        PlayerPrefs.SetFloat(LevelName + " HighScore",hs);
    }
}
