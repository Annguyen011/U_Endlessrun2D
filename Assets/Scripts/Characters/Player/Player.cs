using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("# Move info")]
    public bool runBegun;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float inputX;

    [Header("# Collider info")]
    [SerializeField] private float checkDisctance;
    [SerializeField] private LayerMask whatisground;
    private bool isGrounded;

    #region Components
    private Rigidbody2D rb;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.freezeRotation = true;
        rb.gravityScale = 3f;
    }

    private void Update()
    {
        CheckCollider();

        if (runBegun)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
        }

        CheckInput();
    }

    private void CheckCollider()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, checkDisctance, whatisground);
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            runBegun = !runBegun;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - checkDisctance));
    }
}
