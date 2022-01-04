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
        spawnCoroutine = null;
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
                float SpawnChacne = (float)Random.value * 100f;
                int Combo = ScoreController.main.Combo;
                if (SpawnChacne > 30 + Combo * 2)
                    SpawnNewBug("FruitFly");
                else if (SpawnChacne > 10 + Combo)
                    SpawnNewBug("ButterFly");
                else
                    SpawnNewBug("DragonFly");
                yield return new WaitForSeconds(BugSpawnTime);
            }
            else
            {
                yield return new WaitForSeconds(.5f);
            }
            goto spawn_start;
        }
    }

    public List<ScoreBugController> pickupList = new List<ScoreBugController>();

    public ScoreBugController PoolBug(string bugName)
    {
        for (int child = 0; child<transform.childCount; child++)
        {
            Transform childTransform = transform.GetChild(child);
            if (!childTransform.gameObject.activeInHierarchy && childTransform.name == bugName)
            {
                return childTransform.GetComponent<ScoreBugController>();
            }
        }
        GameObject bug = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Arcade/Pickups/" + bugName));
        bug.name = bugName;
        bug.transform.SetParent(transform);
        bug.SetActive(false);
        return bug.GetComponent<ScoreBugController>();
    }
    public void SpawnNewBug(string bug)
    {
        ScoreBugController newBug = PoolBug(bug);
        newBug.gameObject.SetActive(true);
    }
    public int MaxBugsOnScreen = 1;
    public int GetBugsOnScreen()
    {
        int nBugs = 0;
        for (int child = 0; child < transform.childCount; child++)
        {
            Transform childTransform = transform.GetChild(child);
            if (childTransform.gameObject.activeInHierarchy && childTransform.GetComponent<ScoreBugController>()!=null)
            {
                nBugs++;
            }
        }
        return nBugs;
    }
    public void ClearBugs()
    {
        for (int child = 0; child < transform.childCount; child++)
        {
            Transform childTransform = transform.GetChild(child);
            if (childTransform.gameObject.activeInHierarchy)
            {
                childTransform.gameObject.SetActive(false);
            }
        }
    }
}
