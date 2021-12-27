using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public int CheckPointID = 0;
    Animator ac;


    private void Start()
    {
        ac = GetComponent<Animator>();
        /*  if (CheckPointID >= 0 && CheckPointID <= PlayerPrefs.GetInt(LevelController.main.GetLevelName() + " CheckpointProgress"))
          {
              ac.Play("activated");
              SetEmpty(true);
          }*/
        SetEmpty(false);
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
        ac?.SetBool("activated", value);
    }
}
