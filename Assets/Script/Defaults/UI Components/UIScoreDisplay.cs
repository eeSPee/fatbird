using UnityEngine;
using UnityEngine.UI;

public class UIScoreDisplay : MonoBehaviour
{
    public Text ScoreCounter;
    public Text ComboCounter;
    public GameObject ComboHelp;

    public void Update()
    {
        ScoreCounter.text = ScoreController.main.GetTotalScore().ToString();

        ComboCounter.gameObject.SetActive(ScoreController.main.Combo > 0);
        ComboHelp.gameObject.SetActive(ScoreController.main.Combo > 0);
        ComboCounter.text = "x" + ScoreController.main.Combo;
    }
}
