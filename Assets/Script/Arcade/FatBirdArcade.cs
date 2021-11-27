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
    public int RotationScore = 100;
    public float RotationAngle = 330;
    float LastRotation = 0;
    float RotationDelta = 0;
    float TotalRotation = 0;
    public override void Update()
    {
        base.Update();

        float newDelta = Mathf.DeltaAngle(LastRotation,rbody.rotation);
        if ((newDelta < 0 && RotationDelta>0) || (newDelta > 0 && RotationDelta < 0) || IsGrounded())
        {
            TotalRotation = 0;
        }
        else
        {
            TotalRotation += RotationDelta;
            if (TotalRotation<-RotationAngle)
            {
                TotalRotation += RotationAngle;
                ScoreFlip();
            }
            else if (TotalRotation > RotationAngle)
            {
                TotalRotation -= RotationAngle;
                ScoreFlip();
            }
        }
        RotationDelta = newDelta;
        LastRotation = rbody.rotation;
    }

    public void ScoreFlip()
    {
        if (IsGrounded())
            return;
        SpecialEffectPooler.main.SpawnNewBug("BugPickup", transform.position);
        SpecialEffectPooler.main.TextEffect("FLIP!", transform.position + Vector3.up * .33f);
        SpecialEffectPooler.main.TextEffect("+" + ScoreController.main.ScorePoints(RotationScore, false), transform.position);
    }
}
