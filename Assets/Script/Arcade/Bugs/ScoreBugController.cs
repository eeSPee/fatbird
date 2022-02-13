using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScoreBugController : MonoBehaviour
{
    SpriteRenderer renderer;
    public float Speed = .33f;
    public float OrbitRange = 1;
    public int ScoreValue = 100;

    protected AudioClip audioClip_eat;

    protected float Variation = 1f;
    protected bool moveRight = false;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        audioClip_eat = PlayerController.main.AudioClipEat;
    }
    void OnEnable()
    {
        OnSpawn();
    }
    public void FixedUpdate()
    {
        OnMovement();
        if (LastAiThinkTime < Time.time)
        {
            LastAiThinkTime = Time.time + .33f;
            OnAI();
        }
    }

    public virtual void OnSpawn()
    {
        Variation = Random.value;
        moveRight = PlayerController.main.transform.position.x > 0;
        renderer.flipX = moveRight;
        transform.position = new Vector3(
            -(Camera.main.aspect * Camera.main.orthographicSize + 1) * (moveRight ? 1 : -1),
            Random.value * 4 - 1,
            1
           );
    }
    public virtual void OnMovement()
    {
    }
    protected float LastAiThinkTime = 0;
    public virtual void OnAI()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !LevelController.main.IsGameOver())
        {
            SpecialEffectPooler.main.CreateSpecialEffect("BugPickup", transform.position);
            PlayerController.main.EatBug();
            AudioSourceControllerAndroid.current.Player.PlayOneShot(audioClip_eat);
            Kill(true);
        }
    }
    public void Kill(bool playerscore)
    {
        if (playerscore)
        {
            SpecialEffectPooler.main.TextEffect("+" + ScoreController.main.ScorePoints(ScoreValue, true), transform.position);
        }
        gameObject.SetActive(false);
    }
    public bool IsOutsideGameBounds()
    {
        return Mathf.Abs(transform.position.x)> Camera.main.aspect * Camera.main.orthographicSize + 2;
    }
    public virtual Vector3 HandleOrbit()
    {
        return Vector2.up * Mathf.Sin((Time.time + Variation) % 2 * Mathf.PI) * OrbitRange;
    }
}
