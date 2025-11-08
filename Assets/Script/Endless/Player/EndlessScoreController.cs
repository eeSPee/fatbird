
using UnityEngine;

public class EndlessScoreController : ScoreController
{
    public override float GetTotalScore()
    {
        if (PlayerController.main is PlayerController_Endless endless)
            return Mathf.Floor((Score + endless.GetScoreHeight() * ScorePerSecond)*10) / 10;
        return Score;
    }
}
