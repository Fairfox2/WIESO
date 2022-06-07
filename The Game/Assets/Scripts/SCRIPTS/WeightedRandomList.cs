using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeightedRandomList<T>
{
    [System.Serializable]
    public struct Pair
    {
        public T item;
        public float weight;

        public Pair(T item, float weight)
        {
            this.item = item;
            this.weight = weight;
        }
    }

    public List<Pair> list = new List<Pair>();

    public int Count
    {
        get => list.Count;
    }

    public void Add(T item, float weight)
    {
        list.Add(new Pair(item, weight));
    }

    public int GetRandom()
    {
        float totalWeight = 0;

        foreach (Pair p in list)
        {
            totalWeight += p.weight;
        }

        float value = Random.value * totalWeight;

        float sumWeight = 0;
        int count = 0;
        foreach (Pair p in list)
        {
            sumWeight += p.weight;

            if (sumWeight >= value)
            {
                return count;
            }
            count++;
        }
        return 0;
    }
    public T GetRandom1()
    {
        float totalWeight = 0;

        foreach (Pair p in list)
        {
            totalWeight += p.weight;
        }

        float value = Random.value * totalWeight;

        float sumWeight = 0;
        int count = 0;
        foreach (Pair p in list)
        {
            sumWeight += p.weight;

            if (sumWeight >= value)
            {
                return p.item;
            }
            count++;
        }
        return default(T);
    }
    public T Get(int f)
    {
        if(f <= 0 || f == null) {  f = 0; }
        if(f-1 > list.Count) { f = list.Count; }
        T i;
        try
        {

            i = list[f].item;
        }
        catch
        {
            return default;
        }
        return i;
    }
}
