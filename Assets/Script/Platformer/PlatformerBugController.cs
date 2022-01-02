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
    public bool wasRemoved = false;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();


    }
    private void Start()
    {
        /*if (WasCollected())
        {
            RemoveFromTheGame();
        }*/
        PlayerPrefs.SetInt(LevelController.main.GetLevelName() + " Bug" + transform.GetSiblingIndex(), 0);
        audioSource_eat = FatBirdController.main.AudioSource;
        audioClip_eat = FatBirdController.main.AudioClipEat;
        orbitCenter = transform.position;
        OrbitTime *= OrbitRange * Mathf.PI * 2;
        OrbitRandom = Random.value * OrbitTime;
    }
    public bool WasCollected()
    {
        return PlayerPrefs.GetInt(LevelController.main.GetLevelName() + " Bug" + transform.GetSiblingIndex()) == 1;
    }
    public void RemoveFromTheGame()
    {
        gameObject.SetActive(false);
        wasRemoved = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !LevelController.main.IsGameOver())
        {
            OnEaten();
        }
    }
public void OnEaten()
    {
        SpecialEffectPooler.main.CreateSpecialEffect("BugPickup", transform.position);
        FatBirdController.main.EatBug();
        audioSource_eat.PlayOneShot(audioClip_eat);
        gameObject.SetActive(false);
        
        SpecialEffectPooler.main.TextEffect("NOM!", transform.position);
        BugCounter.main?.IncreaseScore();
        PlayerPrefs.SetInt(LevelController.main.GetLevelName() + " Bug" + transform.GetSiblingIndex(),1);
        RemoveFromTheGame();
    }
    private void FixedUpdate()
    {
        float timedelta = ((Time.time + OrbitRandom) % OrbitTime) / OrbitTime * Mathf.PI*2;
        transform.position = orbitCenter+ new Vector3(Mathf.Sin(timedelta), Mathf.Cos(timedelta),0) * OrbitRange ;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, OrbitRange);
    }
}
