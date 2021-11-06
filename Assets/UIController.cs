using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    Text ScoreCounter;
    Text ComboCounter;
    GameObject ComboHelp;
    List<GameObject> Tutorial = new List<GameObject>();

    public static UIController main;
    private void Awake()
    {
        main = this;
        ScoreCounter = transform.Find("Score Counter").GetComponent<Text>();
        ComboCounter = transform.Find("Combo Counter").GetComponent<Text>();
        ComboHelp = transform.Find("Combo Title").gameObject;

        Tutorial.Add(transform.Find("Tutorial 1").gameObject);
        Tutorial.Add(transform.Find("Tutorial 2").gameObject);
        Tutorial.Add(transform.Find("Tutorial 3").gameObject);
    }
    private void Start()
    {
        UpdateScore();
    }

    public void DisableTutorial()
    {
        foreach (GameObject tutorial in Tutorial)
        {
            tutorial.SetActive(false);
        }
    }
    public void UpdateScore()
    {
        ScoreCounter.text = ScoreController.main.Score.ToString();

        ComboCounter.gameObject.SetActive(ScoreController.main.Combo>0);
        ComboHelp.gameObject.SetActive(ScoreController.main.Combo>0);
        ComboCounter.text = ScoreController.main.Combo.ToString();
    }
}
