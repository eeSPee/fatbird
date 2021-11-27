using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreDisplay : MonoBehaviour
{
    Text ScoreCounter;
    Text ComboCounter;
    GameObject ComboHelp;

    void Awake()
    {
        ScoreCounter = transform.Find("Score Counter").GetComponent<Text>();
        ComboCounter = transform.Find("Combo Counter").GetComponent<Text>();
        ComboHelp = transform.Find("ComboBG").gameObject;
    }
    public void Update()
    {
        ScoreCounter.text = ScoreController.main.Score.ToString();

        ComboCounter.gameObject.SetActive(ScoreController.main.Combo > 0);
        ComboHelp.gameObject.SetActive(ScoreController.main.Combo > 0);
        ComboCounter.text = "x" + ScoreController.main.Combo;
    }
}
