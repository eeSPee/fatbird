using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperObstacleController : MonoBehaviour
{
    Animator BumperAnimator;
    protected AudioSource audioSource_bump;
    protected AudioClip audioClip_bump;
    void Awake()
    {
        BumperAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        audioSource_bump = PlayerController.main.AudioSource;
        audioClip_bump = PlayerController.main.AudioClipBump;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BumperAnimator.SetTrigger("Bump");
        audioSource_bump.PlayOneShot(audioClip_bump);
    }
}
