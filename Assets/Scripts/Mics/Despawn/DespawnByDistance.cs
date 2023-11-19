using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : Despawn
{
    private void Update()
    {
        if (Vector2.Distance(player.position, transform.position) > condition)
        {
            DespawnObject();
        }
    }
}
