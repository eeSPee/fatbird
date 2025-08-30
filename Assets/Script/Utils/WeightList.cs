using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class WeightList 
{
    [System.Serializable]
    public class WeightEntry
    {
        public float Weight = 1;

        public WeightEntry(float weight)
        {
            Weight = Mathf.Max(1,weight);
        }
    }
    [System.Serializable]
    public class WeightPrefab : WeightEntry
    {
        public GameObject prefab;
        public WeightPrefab(GameObject enemyPrefab, float weight) : base(weight)
        {
            prefab = enemyPrefab;
        }
    }
    public class WeightItem<itemt> : WeightEntry
    {
        public itemt value;
        public WeightItem(itemt value, float weight) : base(weight)
        {
            this.value = value;
        }
    }
    public static witem PickWeight<witem>(IEnumerable<witem> list) where witem : WeightEntry
    {
        if (list != null)
        {
            float total = Random.Range(0, GetTotal(list));

            foreach (witem item in list)
            {
                total -= Mathf.Max(.001f, item.Weight);
                if (total <= 0)
                    return item;
            }
        }
        return null;
    }
    public static witem PickWeightCombined<witem>(params IEnumerable<witem>[] lists) where witem : WeightEntry
    {
        if (lists == null || lists.Length == 0)
            return null;

        float totalWeight = 0;

        // Calculate the combined total weight of all items across all lists
        foreach (var list in lists)
        {
            if (list != null)
                totalWeight += GetTotal(list);
        }

        // Generate a random float up to the total weight
        float targetWeight = Random.Range(0, totalWeight);

        // Iterate through each list and each item, decrementing the target weight
        foreach (var list in lists)
        {
            if (list == null) continue;

            foreach (witem item in list)
            {
                targetWeight -= Mathf.Max(.001f, item.Weight);
                if (targetWeight <= 0)
                    return item;
            }
        }

        // If no item was picked, return null
        return null;
    }

    public static float GetTotal(IEnumerable<WeightEntry>  value) 
    {
        float total = 0;
        foreach (WeightEntry item in value)
        {
            total += Mathf.Max(.001f, item.Weight);
        }
        return total;
    }

    public static void Reverse( IEnumerable<WeightEntry> list)
    {
        float total = GetTotal(list);

        foreach (var item in list)
        {
            item.Weight = total - item.Weight;
        }
    }
    public static IEnumerable<witem> PickWeightMultiple<witem>(IEnumerable<witem> list, int amt, bool overlap = false) where witem : WeightEntry
    {
        ICollection<witem> picked = overlap ? new List<witem>() : new HashSet<witem>();

        while (picked.Count < amt)
        {
            picked.Add(PickWeight(list));
        }

        return picked.ToArray();
    }
}
