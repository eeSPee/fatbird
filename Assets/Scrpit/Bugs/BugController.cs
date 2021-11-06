using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BugController : MonoBehaviour
{
    Renderer renderer;
    public float Speed = .33f;
    public float OrbitRange = 1;
    public int ScoreValue = 100;
    protected float Variation = 1f;
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }
    void OnEnable()
    {
        OnSpawn();
        Variation = Random.value;
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
        if (collision.gameObject.tag == "Player")
        {
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
        return !renderer.isVisible;
    }
    public virtual Vector3 HandleOrbit()
    {
        return Vector2.up * Mathf.Sin((Time.time + Variation) % 2 * Mathf.PI) * OrbitRange;
    }
}
