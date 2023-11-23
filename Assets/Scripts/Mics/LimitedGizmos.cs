using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedGizmos : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private Transform groundLevel;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(start.position, new Vector3(start.position.x + 1000, start.position.y));
        Gizmos.DrawLine(start.position, new Vector3(start.position.x - 1000, start.position.y));
        
        Gizmos.DrawLine(end.position, new Vector3(end.position.x , end.position.y+ 1000));
        Gizmos.DrawLine(end.position, new Vector3(end.position.x , end.position.y- 1000));

        Gizmos.DrawLine(groundLevel.position, new Vector3(groundLevel.position.x, groundLevel.position.y + 1000));
        Gizmos.DrawLine(groundLevel.position, new Vector3(groundLevel.position.x, groundLevel.position.y - 1000));
    }
}
