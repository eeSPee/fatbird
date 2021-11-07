using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectPooler : MonoBehaviour
{
    public static SpecialEffectPooler main;
    private void Awake()
    {
        main = this;
    }
    public GameObject PoolEffect(string bugName)
    {
        for (int child = 0; child < transform.childCount; child++)
        {
            Transform childTransform = transform.GetChild(child);
            if (!childTransform.gameObject.activeInHierarchy && childTransform.name == bugName)
            {
                return childTransform.gameObject;
            }
        }
        GameObject bug = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Special Effects/" + bugName));
        bug.name = bugName;
        bug.transform.SetParent(transform);
        bug.SetActive(false);
        return bug;
    }
    public void SpawnNewBug(string bug, Vector3 pos)
    {
        GameObject newBug = PoolEffect(bug);
        newBug.SetActive(true);
        newBug.transform.position = pos;
    }

}
