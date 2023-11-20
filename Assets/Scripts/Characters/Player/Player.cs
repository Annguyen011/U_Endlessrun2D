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
    public bool PlayerMoving => rb.velocity.x > 0;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    private bool canDoubleJump;
    private float inputX;

    [Header("Slide info")]
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideTimer;
    private float slideTimeCounter;
    private bool isSliding;

    [Header("# Collider info")]
    [SerializeField] private LayerMask whatisground;
    [SerializeField] private Transform checkGround;
    [SerializeField] private Transform checkWall;
    [SerializeField] private float checkDisctance;
    [SerializeField] private Vector2 wallCheckSize;
    private bool wallDetected;
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

        if (playerUnlock && !wallDetected)
        {
            Movement();
        }

        CheckInput();
    }


    private void AnimatorController()
    {
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isDoubleJump", canDoubleJump);
    }

    private void CheckCollider()
    {
        isGrounded = Physics2D.OverlapCircle(checkGround.position, checkDisctance, whatisground);
        wallDetected = Physics2D.BoxCast(checkWall.position, wallCheckSize, 0, Vector2.zero, 0, whatisground);
    }

    private void CheckInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        if (isGrounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            playerUnlock = !playerUnlock;
        }

        if (Input.GetButtonDown("Jump"))
        {
            JumpButton();
        }
    }
    private void Movement()
    {
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
    }

    private void JumpButton()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkGround.position, checkDisctance);
        Gizmos.DrawWireCube(checkWall.position, wallCheckSize);
    }
}
