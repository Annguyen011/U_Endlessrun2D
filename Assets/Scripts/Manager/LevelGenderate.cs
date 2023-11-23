using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenderate : MonoBehaviour
{
    [Header("#Level info")]
    [SerializeField] private Vector3 nextPartPosition;

    [Header("# Genderate info")]
    [SerializeField] private Transform player;
    [SerializeField] private float distanceToSpawn;
    [SerializeField] private float distanceToDelete;

    private int ranPlatform;
    private ObjectPoolType type;

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

            Vector2 newPosition = new Vector2(nextPartPosition.x - PoolingOjbect.instance.Get(ObjectPoolType.Platform1)
                .transform.GetChild(1).position.x, 0);

            ranPlatform = UnityEngine.Random.Range(0, 10);

            switch (ranPlatform)
            {
                case 0:
                case 4:
                    type = ObjectPoolType.Platform2;
                    break;

                case 1:
                case 2:
                    type = ObjectPoolType.Platform3;
                    break;

                case 3:
                case 5:
                    type = ObjectPoolType.Platform4;
                    break;
                case 6:
                case 7:

                    type = ObjectPoolType.Platform5;
                    break;

                case 8:
                case 9:
                    type = ObjectPoolType.Platform6;
                    break;

                default:
                    type = ObjectPoolType.Platform1;
                    break;
            }

            Transform newPart = PoolingOjbect.instance.Get(type).transform;
            newPart.position = newPosition;

            nextPartPosition = newPart.GetChild(2).position;
        }
    }

}
