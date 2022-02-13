using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperObstacleController : MonoBehaviour
{
    Animator BumperAnimator;
    protected AudioClip audioClip_bump;
    void Awake()
    {
        BumperAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        audioClip_bump = PlayerController.main.AudioClipBump;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BumperAnimator.SetTrigger("Bump");
        AudioSourceControllerAndroid.current.Effects.PlayOneShot(audioClip_bump);
    }
}
