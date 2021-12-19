using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public int CheckPointID = 0;
    Animator ac;


    private void Awake()
    {
        ac = GetComponent<Animator>();
        if (CheckPointID == 0)
            SetEmpty(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelController.main.PlayerLeaveSafezone();
        }
    }
    public void SetEmpty(bool value)
    {
        ac.SetBool("isempty", value);
    }
}
