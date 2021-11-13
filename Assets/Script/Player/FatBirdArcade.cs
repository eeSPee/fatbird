using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdArcade : FatBirdController
{
    public override void OnGrounded()
    {
        if (ScoreController.main.Combo > 0)
        {
            ScoreController.main.ResetCombo();
        }
    }
}
