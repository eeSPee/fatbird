using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerBugController : MonoBehaviour
{
    float OrbitRandom = 0;
    Vector3 orbitCenter;
    public float OrbitRange = 1;
    public float OrbitTime = 2;

    SpriteRenderer renderer;
    protected AudioSource audioSource_eat;
    protected AudioClip audioClip_eat;
    public bool wasCollected = false;
    public bool wasRemoved = false;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();


    }
    private void Start()
    {
        audioSource_eat = FatBirdController.main.AudioSource;
        audioClip_eat = FatBirdController.main.AudioClipEat;
        orbitCenter = transform.position;
        OrbitRandom = Random.value * OrbitTime;
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
            SpecialEffectPooler.main.TextEffect("MUNCH!", transform.position);
            wasCollected = true;
        }
    }
    private void FixedUpdate()
    {
        float timedelta = (Time.time + OrbitRandom) % OrbitTime * Mathf.PI;
        transform.position = orbitCenter+ new Vector3(Mathf.Sin(timedelta), Mathf.Cos(timedelta),0) * OrbitRange ;
    }
}
