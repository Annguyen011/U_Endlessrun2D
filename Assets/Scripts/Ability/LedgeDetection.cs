using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    [Header("# Ledge info")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatisground;
    private bool canDetected;

    #region Components
    private Player player;
    //private BoxCollider2D boxCollider;
    #endregion

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        //boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (canDetected)
        {
            player.ledgeDetected = Physics2D.OverlapCircle(transform.position, radius, whatisground);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            canDetected = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCollider.bounds.center, boxCollider.size, 0);
        
        //foreach (Collider2D collider in colliders)
        //{

        //}

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            canDetected = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
