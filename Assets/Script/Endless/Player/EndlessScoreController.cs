
public class EndlessScoreController : ScoreController
{
    public override float GetTotalScore()
    {
        return Score + (PlayerController.main.transform.position.y + 3) * ScorePerSecond;
    }
}
