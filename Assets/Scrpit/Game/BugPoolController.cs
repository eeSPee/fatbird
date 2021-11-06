using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugPoolController : MonoBehaviour
{
    public float BugStartTime = 5;
    public float BugSpawnTime = 10;
    public static BugPoolController main;
    private void Awake()
    {
        main = this;
    }

    Coroutine spawnCoroutine;
    public void StartSpawning()
    {
        StopSpawning();
        spawnCoroutine = StartCoroutine(SpawnBugs());
    }
    public void StopSpawning()
    {
        if (IsSpawning())
            StopCoroutine(spawnCoroutine);
    }
    public bool IsSpawning()
    {
        return (spawnCoroutine != null);
    }
    public IEnumerator SpawnBugs()
    {
        yield return new WaitForSeconds(BugStartTime);
        spawn_start:
        {
            if (GetBugsOnScreen() < MaxBugsOnScreen)
            {
                float SpawnChacne = (float)Random.value;
                if (SpawnChacne < .33f)
                    SpawnNewBug("Simple Bug");
                else if (SpawnChacne < .66f)
                    SpawnNewBug("Smart Bug");
                else
                    SpawnNewBug("Indifferent Bug");
                yield return new WaitForSeconds(BugSpawnTime);
            }
            else
            {
                yield return new WaitForSeconds(.5f);
            }
            goto spawn_start;
        }
    }

    public List<BugController> pickupList = new List<BugController>();

    public BugController PoolBug(string bugName)
    {
        for (int child = 0; child<transform.childCount; child++)
        {
            Transform childTransform = transform.GetChild(child);
            if (!childTransform.gameObject.activeInHierarchy && childTransform.name == bugName)
            {
                return childTransform.GetComponent<BugController>();
            }
        }
        GameObject bug = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Pickups/"+ bugName));
        bug.name = bugName;
        bug.transform.SetParent(transform);
        bug.SetActive(false);
        return bug.GetComponent<BugController>();
    }
    public void SpawnNewBug(string bug)
    {
        BugController newBug = PoolBug(bug);
        newBug.gameObject.SetActive(true);
    }
    public int MaxBugsOnScreen = 1;
    public int GetBugsOnScreen()
    {
        int nBugs = 0;
        for (int child = 0; child < transform.childCount; child++)
        {
            Transform childTransform = transform.GetChild(child);
            if (childTransform.gameObject.activeInHierarchy)
            {
                nBugs++;
            }
        }
        return nBugs;
    }
}
