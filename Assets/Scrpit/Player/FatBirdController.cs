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
            FlapLeftWing();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            FlapRightWing();
        }
    }
    public float FlapSpeed = 50;
    public float FlapTorque = 33;
    public float FlapStamina = 20;
    public float FlapWeak = .33f;
    void FlapLeftWing()
    {
        float fMult = ChargeStamina(FlapStamina) ? 1 : FlapWeak;
        rbody.AddForce(transform.up * FlapSpeed * fMult);
        rbody.AddTorque(FlapTorque * fMult);
        anim.SetTrigger("FlapLeft");
    }
    void FlapRightWing()
    {
        float fMult = ChargeStamina(FlapStamina) ?1: FlapWeak;
        rbody.AddForce(transform.up * FlapSpeed * fMult);
        rbody.AddTorque(-FlapTorque * fMult);
        anim.SetTrigger("FlapRight");
    }

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
