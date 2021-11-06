using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugPoolController : MonoBehaviour
{
 float NextBugSpawnTime = 0;
    public float BugStartTime = 5;
    public float BugSpawnTime = 10;
    private void OnEnable()
    {
        NextBugSpawnTime = BugSpawnTime;
    }
    private void FixedUpdate()
    {
        if (Time.time> NextBugSpawnTime)
        {
            SpawnNewBug("Bug");
            NextBugSpawnTime = Time.time + BugSpawnTime;
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
}
