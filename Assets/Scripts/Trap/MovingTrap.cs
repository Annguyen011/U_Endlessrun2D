using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : Trap
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private List<Transform> movePoints = new List<Transform>();

    private int indexPoint = 0;
    private bool changeDirection;

    private void Start()
    {
        indexPoint = 0;
        transform.position = movePoints[indexPoint].position;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePoints[indexPoint].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoints[indexPoint].position) < .25f)
        {
            if (indexPoint == movePoints.Count) changeDirection = true;
            else if (indexPoint == 0) changeDirection = false;

            indexPoint += (changeDirection) ? -1 : 1;
        }

        transform.Rotate(0, 0, (changeDirection) ? rotationSpeed : -rotationSpeed);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
