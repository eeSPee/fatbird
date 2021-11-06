using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rbody;
    private void Awake()
    {
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
    float FlapSpeed = 50;
    float FlapTorque = 33;
    void FlapLeftWing()
    {
        rbody.AddForce(transform.up * FlapSpeed);
        rbody.AddTorque(FlapTorque);
        anim.SetTrigger("FlapLeft");
    }
    void FlapRightWing()
    {
        rbody.AddForce(transform.up * FlapSpeed);
        rbody.AddTorque(-FlapTorque);
        anim.SetTrigger("FlapRight");
    }
}
