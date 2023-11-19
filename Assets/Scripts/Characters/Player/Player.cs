using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [Header("# Move info")]
    public bool playerUnlock;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float inputX;

    [Header("# Collider info")]
    [SerializeField] private LayerMask whatisground;
    [SerializeField] private Transform checkGround;
    [SerializeField] private float checkDisctance;
    private bool isGrounded;

    #region Components
    private Rigidbody2D rb;
    private Animator animator;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        rb.freezeRotation = true;
        rb.gravityScale = 3f;
    }

    private void Update()
    {
        AnimatorController();
        CheckCollider();

        if (playerUnlock)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
        }

        CheckInput();
    }

    private void AnimatorController()
    {
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isGrounded", isGrounded);
    }

    private void CheckCollider()
    {
        isGrounded = Physics2D.OverlapCircle(checkGround.position, checkDisctance, whatisground);
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            playerUnlock = !playerUnlock;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkGround.position, checkDisctance);
    }
}
