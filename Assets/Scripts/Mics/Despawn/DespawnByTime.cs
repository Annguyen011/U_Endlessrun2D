using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : Despawn
{
    protected override void OnEnable()
    {
        base.OnEnable();

        Invoke(nameof(DespawnObject), condition);
    }
}
