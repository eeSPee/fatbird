using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Components")]
    public Transform activeObjs;
    public Transform inactiveObjs;

    public virtual GameObject PoolItem(GameObject Prefab, bool fresh = false)
    {
        if (!fresh)
        {
            foreach (Transform child in inactiveObjs)
            {
                if (child != null && !child.gameObject.activeSelf
                    && Prefab.name.Length <= child.name.Length
                        && Prefab.name == child.name.Substring(0, Prefab.name.Length))
                {
                    ActivateObject(child.gameObject);
                    return child.gameObject;
                }
            }
        }
        return InitFromPrefab(Prefab);
    }
    protected virtual GameObject InitFromPrefab(GameObject Prefab)
    {
        GameObject nObject = GameObject.Instantiate(Prefab);
        nObject.name = Prefab.name;
        ActivateObject(nObject);
        return nObject;
    }
    public virtual void ActivateObject(GameObject gOb)
    {
        if (gOb == null) return;
        gOb.transform.SetParent(activeObjs);
        gOb.SetActive(true);
    }
    public virtual void DeactivateObject(GameObject gOb, bool changeActive = true)
    {
        if (gOb == null) return;
        if (gOb.transform.parent != inactiveObjs)
        {
            gOb.transform.SetParent(inactiveObjs);
        }
        if (changeActive)
            gOb.SetActive(false);
    }


    public int GetNActiveObjects()
    {
        return activeObjs.childCount;
    }
    [Header("Precache")]
    public CacheData[] precacheData;
    [System.Serializable]
    public class CacheData
    {
        public GameObject cachePrefab;
        public int cacheAmount;
    }
    private void Awake()
    {
        Precache(precacheData);
    }
    void Precache(CacheData[] stuff)
    {
        List<GameObject> items = new List<GameObject>();
        foreach (var item in stuff)
        {
            for (int i = 0; i < item.cacheAmount; i++)
            {
                items.Add(PoolItem(item.cachePrefab));
            }
        }
        foreach (var erase in items)
        {
            DeactivateObject(erase);
        }
    }
}
