using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public int CheckPointID = 0;

    public static CheckPointController GetCheckpointByID(int ID)
    {
        foreach (CheckPointController checkpoint in Object.FindObjectsOfType<CheckPointController>())
        {
            if (checkpoint.CheckPointID == ID)
            { return checkpoint; }
        }
        return null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelController.main.ResumeGame();
        }
    }
}
