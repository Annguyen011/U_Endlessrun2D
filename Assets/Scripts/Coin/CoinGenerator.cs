using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private int amountOfCoints;


    [SerializeField] private float chanceToSpawn;
    [SerializeField] private int minCoin;
    [SerializeField] private int maxCoin;

    [SerializeField] private SpriteRenderer[] coinImg;
    private void Start()
    {
        for (int i = 0; i < coinImg.Length; i++)
        {
            coinImg[i].sprite = null;
        }

        amountOfCoints = Random.Range(minCoin, maxCoin);
        int additionalOffset = amountOfCoints / 2;
        bool canSpawn = chanceToSpawn > Random.Range(0, 100);

        if (!canSpawn) return;

        for (int i = 0; i < amountOfCoints; i++)
        {
            Vector3 offset = new Vector3(i - additionalOffset, 0, 0);

            GameObject newCoin = PoolingOjbect.instance.Get(ObjectPoolType.Coin);
            newCoin.transform.position = transform.position + offset;
        }
    }
}
