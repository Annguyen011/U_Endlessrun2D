using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingOjbect : Singleton<PoolingOjbect>
{
    [SerializeField] private GameObject[] prefabs;
    private List<GameObject>[] pools;

    protected override void Awake()
    {
        base.Awake();

        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(ObjectPoolType type)
    {
        GameObject select = null;

        foreach (GameObject item in pools[(int)type])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);

                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[(int)type]);
            pools[(int)type].Add(select);
            select.transform.parent = transform;
        }

        return select;
    }
}
public enum ObjectPoolType
{
    Platform1,
    Platform2,
    Platform3,
    Platform4,
    Platform5,
    Platform6,
    Coin
}
