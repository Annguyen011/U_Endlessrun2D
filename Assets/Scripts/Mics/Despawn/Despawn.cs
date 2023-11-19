using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] protected float condition;

    protected virtual void OnEnable()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    protected virtual void DespawnObject()
    {
        gameObject.SetActive(false);
    }
}
