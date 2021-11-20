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
    public GameObject PoolEffect(string effectName)
    {
        for (int child = 0; child < transform.childCount; child++)
        {
            Transform childTransform = transform.GetChild(child);
            if (!childTransform.gameObject.activeInHierarchy && childTransform.name == effectName)
            {
                return childTransform.gameObject;
            }
        }
        GameObject bug = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Special Effects/" + effectName));
        bug.name = effectName;
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

    public void TextEffect(string text, Vector2 position)
    {
        GameObject effect = PoolEffect("TextEffect");
        effect.transform.localPosition = new Vector3(position.x, position.y, 0) + new Vector3(1 - Random.value * 2, 1 - Random.value * 2, 0);
        effect.GetComponent<TextMesh>().text = text;
        effect.SetActive(true);       
        effect.GetComponent<Animator>().SetTrigger("Play");
    }

}
