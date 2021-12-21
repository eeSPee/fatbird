using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    protected GameObject Tutorial;
    protected GameObject PauseMenu;

    public static UIController main;
    public virtual void Awake()
    {
        main = this;
        Tutorial = transform.Find("Tutorial").gameObject;
        PauseMenu = transform.Find("Pause Menu").gameObject;
    }
    protected virtual void Start()
    {
        DisableGameOverScreen();
        EnableDisablePauseMenu(false);
    }

        public void DisableTutorial()
    {
        Tutorial.SetActive(false);
    }
    public virtual void EnableGameOverScreen(bool victory)
    {
    }
    public virtual void EnableDisablePauseMenu(bool Pause)
    {
        PauseMenu.gameObject.SetActive(Pause);
    }
    public virtual void DisableGameOverScreen()
    {
    }
}
