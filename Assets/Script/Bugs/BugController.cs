using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BugController : MonoBehaviour
{
    SpriteRenderer renderer;
    public float Speed = .33f;
    public float OrbitRange = 1;
    public int ScoreValue = 100;
    protected float Variation = 1f;
    protected bool moveRight = false;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
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
        moveRight = FatBirdController.main.transform.position.x > 0;
        renderer.flipX = moveRight;
        transform.position = new Vector3(
            -(Camera.main.aspect * Camera.main.orthographicSize + 1) * (moveRight ? 1 : -1),
            Random.value * 5 - 3,
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
        if (collision.gameObject.tag == "Player" && !GameController.main.IsGameOver())
        {
            FatBirdController.main.EatBug();
            Kill(true);
        }
    }
    public void Kill(bool playerscore)
    {
        if (playerscore)
            ScoreController.main.ScorePoints(ScoreValue, true);
        else
            ScoreController.main.ResetCombo();
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
