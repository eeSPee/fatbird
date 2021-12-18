using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerBugController : MonoBehaviour
{
    SpriteRenderer renderer;
    protected AudioSource audioSource_eat;
    protected AudioClip audioClip_eat;
    bool wasCollected = false;
    public bool wasRemoved = false;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        audioSource_eat = FatBirdController.main.AudioSource;
        audioClip_eat = FatBirdController.main.AudioClipEat;

        PlatformerLevelController lc = LevelController.main as PlatformerLevelController;
        if (lc != null)
        {
            lc.bugs.Add(this);
        }
    }
    public void OnEnable()
    {
        wasCollected = false;
    }
    public void RemoveFromTheGame()
    {
        wasRemoved = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !LevelController.main.IsGameOver())
        {
            SpecialEffectPooler.main.CreateSpecialEffect("BugPickup", transform.position);
            FatBirdController.main.EatBug();
            audioSource_eat.PlayOneShot(audioClip_eat);
            gameObject.SetActive(false);
            wasCollected = true;
        }
    }
}
