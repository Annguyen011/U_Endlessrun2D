using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenderate : MonoBehaviour
{
    [Header("#Level info")]
    [SerializeField] private Transform[] levelPart;
    [SerializeField] private Vector3 nextPartPosition;

    [Header("# Genderate info")]
    [SerializeField] private Transform player;
    [SerializeField] private float distanceToSpawn;
    [SerializeField] private float distanceToDelete;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        GeneratePlatform();
    }

    private void GeneratePlatform()
    {
        while (Vector2.Distance(player.position, nextPartPosition) < distanceToSpawn)
        {

            Vector2 newPosition = new Vector2(nextPartPosition.x - PoolingOjbect.instance.Get(ObjectPoolType.Platform)
                .transform.GetChild(1).position.x, 0);

            Transform newPart = PoolingOjbect.instance.Get(ObjectPoolType.Platform).transform;
            newPart.position = newPosition;

            nextPartPosition = newPart.GetChild(2).position;
        }
    }
   
}
