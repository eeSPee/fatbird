using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rbody;
    public static FatBirdController main;
    private void Awake()
    {
        main = this;
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            FlapWing(false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            FlapWing(true);
        }
    }
    public float FlapSpeedInitial = 200;
    public float FlapSpeed = 50;
    public float FlapTorque = 33;
    public float FlapStamina = 20;
    public float FlapWeak = .33f;
    void FlapWing(bool right)
    {
        if (ScoreController.main.IsScoring())
        {
            float fMult = ChargeStamina(FlapStamina) ? 1 : FlapWeak;
        if (transform.up.y > 0)
            rbody.AddForce(transform.up * FlapSpeed * fMult);
        rbody.AddTorque(FlapTorque * fMult * (right ? -1 : 1));
        }
        else
        {
            wingflap[right ? 0 : 1] = Time.time;
            if (Mathf.Abs(wingflap[0] - wingflap[1])<.1f)
            {
                ScoreController.main.StartRecording();
                BugPoolController.main.StartSpawning();
                rbody.AddForce(transform.up * FlapSpeedInitial);
                UIController.main.DisableTutorial();
            }
        }
        anim.SetTrigger("Flap" + (  right ? "Right" : "Left"));
    }
    float[] wingflap = new float[] { 0, 0 };

    public float Stamina = 100;
    public float StaminaRegen = 75;

    public bool ChargeStamina(float amount)
    {
        if (Stamina<amount)
        {
            Stamina = 0;
            return false;
        }
        Stamina -= amount;
        return true;
    }
    private void FixedUpdate()
    {
        Stamina = Mathf.Min(Stamina + StaminaRegen * Time.deltaTime,100);
    }
}
