using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour
{
    public int ScoreValue = 100;
    public float Speed = .33f;
    public float SightRange = 2f;
    void OnEnable()
    {
        OnSpawn();
    }
    public void FixedUpdate()
    {
        OnMovement();
        if (LastAiThinkTime<Time.time)
        {
            LastAiThinkTime = Time.time + .33f;
            OnAI();
        }
    }

    bool moveRight = false;
    public virtual void OnSpawn()
    {
        AiState = BugState.wander;
        moveRight = Random.value < .5f;
        transform.position = new Vector3(
            -(Camera.main.aspect * Camera.main.orthographicSize + 1) * (moveRight ? 1 : -1),
            Random.value * 6 - 2,
            1
           );
    }
    public virtual void OnMovement()
    {
        Vector3 wiggle = Vector2.up * Mathf.Sin(Time.time % 2 * Mathf.PI);
        switch (AiState)
        {
            case BugState.wander:
                transform.position += (Vector3.right * (moveRight ? 1 : -1) + wiggle) * Speed * Time.deltaTime;
                break;
            case BugState.fleeing:
                transform.position -= (Vector3.right * (moveRight ? 1 : -1) + wiggle) * Speed * Time.deltaTime * 1.25f;
                break;
        }
    }
    float LastAiThinkTime = 0;
    enum BugState
    {
        wander,
        curious,
        fleeing
    }
    BugState AiState = BugState.wander;
    public virtual void OnAI()
    {
        switch (AiState)
        {
            case BugState.wander:
                if ((FatBirdController.main.transform.position - transform.position).sqrMagnitude < SightRange * SightRange)
                {
                    AiState = BugState.curious;
                    LastAiThinkTime = Time.time + 5;
                }
                break;
            case BugState.curious:
                AiState = BugState.fleeing;
                break;
            case BugState.fleeing:
                if (IsOutsideGameBounds())
                {
                    ScoreController.main.ResetCombo();
                    Kill();
                }
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ScoreController.main.ScorePoints(ScoreValue,true);
            Kill();
        }
    }
    public void Kill()
    {
        gameObject.SetActive(false);
    }
    public bool IsOutsideGameBounds()
    {
        return (
            Mathf.Abs(transform.position.y) + transform.localScale.y > Camera.main.orthographicSize ||
            Mathf.Abs(transform.position.x) + transform.localScale.x > Camera.main.orthographicSize * Camera.main.aspect
            );
    }
}
