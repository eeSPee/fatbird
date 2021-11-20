using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : ArcadeObstacleController
{
    public bool AlwaysBlows = false;
    public float WindAcceleration = 1f;

    public float RepeatTime = 15f;
    public float UpTime = 3f;

    Collider2D WindTrigger;
    List<ParticleSystem> particles;
    private void Awake()
    {
        WindTrigger = GetComponent<Collider2D>();
        particles = new List<ParticleSystem>();
        particles.AddRange(GetComponentsInChildren<ParticleSystem>());
    }
    private void Start()
    {
        StopBlowing();
    }
    public void StartBlowing()
    {
        WindTrigger.enabled = true;
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
    public void StopBlowing()
    {
        WindTrigger.enabled = false;
        foreach (ParticleSystem particle in particles)
        {
            particle.Stop();
        }
    }
    public override void Reset()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        StopBlowing();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
     if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity += (Vector2)transform.right * WindAcceleration * Time.deltaTime;
        }
    }
    protected override IEnumerator SpawnIntoGame()
    {
        if (AlwaysBlows)
        {
            StartBlowing();
        }
        else
        {
            yield return new WaitForSeconds(ComeInTime);
            blow_cycle:
            {
                StartBlowing();
                yield return new WaitForSeconds(UpTime);
                StopBlowing();
            }

            yield return new WaitForSeconds(RepeatTime);
            goto blow_cycle;
        }

    }
}
