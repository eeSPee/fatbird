using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdController : MonoBehaviour
{
    Vector3 start;
    Animator anim;
    Rigidbody2D rbody;
    public AudioSource audiosource;
    public AudioClip AudioClipLeftWing;
    public AudioClip AudioClipRightWing;
    public AudioClip AudioClipEat;
    public AudioClip AudioClipSpike;
    public AudioClip AudioClipBump;
    public static FatBirdController main;
    private void Awake()
    {
        main = this;
        start = transform.position;
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
    }
    public virtual void Update()
    {
        if (!LevelController.main.IsGameOver())
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
    public virtual void FlapWing(bool right)
    {
        if (LevelController.main.IsGameRunning())
        {
            float fMult = ChargeStamina(FlapStamina) ? 1 : FlapWeak;
            if (!IsGrounded() || transform.up.y > 0 )
                rbody.AddForce(transform.up * FlapSpeed * fMult);
            rbody.AddTorque(FlapTorque * fMult * (right ? -1 : 1));
        }
        else
        {
            wingflap[right ? 0 : 1] = Time.time;
            if (Mathf.Abs(wingflap[0] - wingflap[1]) < .1f)
            {
                LevelController.main.StartTheGame();
                rbody.AddForce(transform.up * FlapSpeedInitial);
                transform.Find("Launch Particle").gameObject.SetActive(true);
            }
        }
        anim.SetTrigger("Flap" + (right ? "Right" : "Left"));

        if (right)
        {
            audiosource.PlayOneShot(AudioClipRightWing);
        }
        else
        {
            audiosource.PlayOneShot(AudioClipLeftWing);
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
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike" && !LevelController.main.IsGameOver())
        {
            audiosource.PlayOneShot(AudioClipSpike);
            LevelController.main.EndTheGame(false);
            transform.Find("Hurt Particle").gameObject.SetActive(true);
            anim.SetBool("Hurt", true);
        }
    }
    float lastGroundTime = 0;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.point.y<transform.position.y + 1)
                {
                    lastGroundTime = Time.time + .1f;
                    OnGrounded();
                    break;
                }
            }
        }
    }
    public bool IsGrounded()
    {
        return lastGroundTime > Time.time;
    }
    public virtual void OnGrounded()
    {
    }
    public void EatBug()
    {
            anim.SetTrigger("EatBug");

    }
    public void Reset()
    {
        transform.position = start;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rbody.velocity = Vector2.zero;
        rbody.angularVelocity = 0;
        anim.SetBool("Hurt", false);
    }
}
