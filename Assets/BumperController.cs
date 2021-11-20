using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperController : MonoBehaviour
{
    Animator BumperAnimator;
    void Awake()
    {
        BumperAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO add sound
        BumperAnimator.SetTrigger("Bump");
    }
}
