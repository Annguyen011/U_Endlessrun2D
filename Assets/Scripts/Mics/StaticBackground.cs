using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBackground : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 offset;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x + offset.x, offset.y);
    }
}
