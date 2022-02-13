using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindObstacleController : MonoBehaviour, IArcadeObstacle
{
    public bool AlwaysBlows = false;
    public float WindAcceleration = 1f;

    public float ComeInTime = 10f;
    public float RepeatTime = 15f;
    public float UpTime = 3f;

    public AudioClip AudioClipWind;
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
        if (!AlwaysBlows)
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
    private void OnTriggerStay2D(Collider2D collision)
    {
     if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity += (Vector2)transform.right * WindAcceleration * Time.deltaTime;
        }
    }
    protected Coroutine spawnCoroutine;
    public void StartSpawning()
    {
        Reset();
        spawnCoroutine = StartCoroutine(SpawnIntoGame());
    }
    public void Reset()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        StopBlowing();
    }
    protected IEnumerator SpawnIntoGame()
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
                AudioSourceControllerAndroid.current.Environment.Play();
                yield return new WaitForSeconds(UpTime);
                StopBlowing();
            }

            yield return new WaitForSeconds(RepeatTime);
            goto blow_cycle;
        }

    }
//    public void UpdateListener()
//      {
//        float ListenerDistance = ((Vector2)transform.position - (Vector2)FatBirdController.player.transform.position).magnitude;
//        AudioSourceWind.volume = Mathf.Clamp(1 - ((ListenerDistance-FatBirdController.MinListenerDistance) / (FatBirdController.MaxListenerDistance - FatBirdController.MinListenerDistance)),0,1);
//      }
}
