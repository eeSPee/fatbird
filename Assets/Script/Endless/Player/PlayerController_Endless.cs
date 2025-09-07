using UnityEngine;

public class PlayerController_Endless : PlayerController_Arcade
{

    private void LateUpdate()
    {
        UpdateCamera();
    }
    public override void OnLevelReset()
    {
        if (CameraController.main != null)
        {
            CameraController.main.transform.position = Vector3.zero;
        }
        base.OnLevelReset();
    }
    void UpdateCamera()
    {
        if (CameraController.main != null)
        {
            CameraController.main.transform.position = new Vector3(0, Mathf.Max(transform.position.y, CameraController.main.transform.position.y), 0);
        }
    }

    public override void Update()
    {
        base.Update();
        CheckDeath();
    }
    void CheckDeath()
    {
        if (CameraController.main != null && transform.position.y < CameraController.main.transform.position.y - CameraController.main.camera.orthographicSize - 1)
            Hurt();
    }
    public override void ScoreFlip()
    {

        if (IsGrounded())
            return;
        SpecialEffectPooler.main.CreateSpecialEffect("BugPickup", transform.position);
        SpecialEffectPooler.main.TextEffect("FLIP!", transform.position + Vector3.up * .33f);
        SpecialEffectPooler.main.TextEffect("+" + ScoreController.main.ScorePoints(RotationScore, false), transform.position);
    }
}
