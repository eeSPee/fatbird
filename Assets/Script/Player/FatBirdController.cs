using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rbody;
    public AudioSource AudioSourceLeftWing;
    public AudioSource AudioSourceRightWing;
    public AudioClip AudioClipLeftWing;
    public AudioClip AudioClipRightWing;
    public static FatBirdController main;
    private void Awake()
    {
        main = this;
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (!GameController.main.IsGameOver())
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                FlapWing(false);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                FlapWing(true);
            }
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
            if (Mathf.Abs(wingflap[0] - wingflap[1]) < .1f)
            {
                GameController.main.StartTheGame();
                rbody.AddForce(transform.up * FlapSpeedInitial);
                transform.Find("Launch Particle").gameObject.SetActive(true);
            }
        }
        anim.SetTrigger("Flap" + (right ? "Right" : "Left"));

        if (right)
        {
            AudioSourceRightWing.PlayOneShot(AudioClipRightWing);
        }
        else
        {
            AudioSourceLeftWing.PlayOneShot(AudioClipLeftWing);
        }
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
        anim.SetBool("IsTired", Stamina< FlapStamina);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ScoreController.main.ResetCombo();
        }
        if (collision.gameObject.tag == "Spike")
        {
            GameController.main.EndTheGame();
            transform.Find("Hurt Particle").gameObject.SetActive(true);
        }
    }
    public void EatBug()
    {
            anim.SetTrigger("EatBug");
        
    }
    public void Reset()
    {
        transform.position = Vector3.down * 2.5f;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rbody.velocity = Vector2.zero;
        rbody.angularVelocity = 0;
    }
}
