using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    GameObject Tutorial;

    public static UIController main;
    public virtual void Awake()
    {
        main = this;
        Tutorial = transform.Find("Tutorial").gameObject;
    }
    void Start()
    {
        DisableGameOverScreen();
    }

    public virtual void Update()
    { }

        public void DisableTutorial()
    {
        Tutorial.SetActive(false);
    }
    public virtual void EnableGameOverScreen(bool victory)
    {
    }
    public virtual void DisableGameOverScreen()
    {
    }
}
