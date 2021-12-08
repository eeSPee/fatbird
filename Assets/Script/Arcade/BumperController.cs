using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperController : MonoBehaviour
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
        audioSource_bump = FatBirdController.main.AudioSource;
        audioClip_bump = FatBirdController.main.AudioClipBump;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BumperAnimator.SetTrigger("Bump");
        audioSource_bump.PlayOneShot(audioClip_bump);
    }
}
