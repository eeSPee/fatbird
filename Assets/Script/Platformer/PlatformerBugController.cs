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
            OnEaten();
        }
    }
    public static string[] eatWords = new string[] { "NOM", "NOM", "NOM", "NOM NOM", "MUNCH"};
public void OnEaten()
    {
        SpecialEffectPooler.main.CreateSpecialEffect("BugPickup", transform.position);
        FatBirdController.main.EatBug();
        audioSource_eat.PlayOneShot(audioClip_eat);
        gameObject.SetActive(false);
        
        SpecialEffectPooler.main.TextEffect(eatWords [(int)(Random.value * (eatWords.Length-1))]+ "!", transform.position);
        wasCollected = true;
        BugCounter.main?.IncreaseScore();
    }
    private void FixedUpdate()
    {
        float timedelta = (Time.time + OrbitRandom) % OrbitTime * Mathf.PI;
        transform.position = orbitCenter+ new Vector3(Mathf.Sin(timedelta), Mathf.Cos(timedelta),0) * OrbitRange ;
    }
}
