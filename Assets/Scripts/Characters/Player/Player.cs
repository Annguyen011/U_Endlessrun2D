using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [Header("# Player info")]
    private bool isDead = false;

    [Header("# Speed info")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedMuitiple;
    [Space]
    [SerializeField] private float milestoneIncreaser;
    private float speedMileStone;
    private float defaultSpeed;
    private float defaultMilestoneIncreaser;

    [Header("# Move info")]
    public bool playerUnlock;
    public bool PlayerMoving => rb.velocity.x > 0;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    private bool canDoubleJump;
    private float inputX;

    [Header("# Slide info")]
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideTime;
    [SerializeField] private float slideCooldown;
    private float slideCooldownCounter;
    private float slideTimeCounter;
    private bool isSliding;

    [Header("# Ledge info")]
    public bool ledgeDetected;
    [SerializeField] private Vector2 offset1;
    [SerializeField] private Vector2 offset2;
    private Vector2 climbBegunPosition;
    private Vector2 climbOverPosition;
    private bool canGrabLedge = true;
    private bool canClimb;

    [Header("# Knockback info")]
    [SerializeField] private Vector2 knockbackDirection;
    private bool isKnocked;
    private bool canBeKnockback = true;

    [Header("# Collider info")]
    [SerializeField] private LayerMask whatisground;
    [SerializeField] private Transform checkGround;
    [SerializeField] private Transform checkWall;
    [SerializeField] private float checkDisctance;
    [SerializeField] private float ceilingcheckDisctance;
    [SerializeField] private Vector2 wallCheckSize;
    private bool ceilingDetected;
    private bool wallDetected;
    private bool isGrounded;



    #region Components
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        rb.freezeRotation = true;
        rb.gravityScale = 3f;

        defaultMilestoneIncreaser = milestoneIncreaser;
        defaultSpeed = moveSpeed;
    }

    private void Update()
    {
        TimeCounter();

        AnimatorController();
        CheckCollider();

        if (isDead) return;
        if (isKnocked) return;

        if (playerUnlock)
        {
            Movement();
        }

        SpeedController();

        CheckInput();
        CheckForSlide();
        CheckForLedge();
    }

    private void TimeCounter()
    {
        slideTimeCounter -= Time.deltaTime;
        slideCooldownCounter -= Time.deltaTime;
    }

    private void AnimatorController()
    {
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("canClimb", canClimb);
        animator.SetBool("isSliding", isSliding);
        animator.SetBool("isKnocked", isKnocked);
        animator.SetBool("isDoubleJump", canDoubleJump);

        if (rb.velocity.y < -20)
        {
            animator.SetBool("canRoll", true);
        }
    }

    private void CheckCollider()
    {
        isGrounded = Physics2D.OverlapCircle(checkGround.position, checkDisctance, whatisground);
        ceilingDetected = Physics2D.Raycast(transform.position, Vector2.up, ceilingcheckDisctance, whatisground);
        wallDetected = Physics2D.BoxCast(checkWall.position, wallCheckSize, 0, Vector2.zero, 0, whatisground);
    }

    private void CheckInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SlideButton();
        }

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

        // Test
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Knockback();
        }
    }

    #region Player state

    public void Damage()
    {
        if(moveSpeed >= maxSpeed)
        {
            Knockback();
        }
        else
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        isDead = true;
        canBeKnockback = false;
        rb.velocity = knockbackDirection;
        animator.SetBool("isDead", true);

        yield return new WaitForSeconds(.1f);

        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(2f);

        GameManager.instance.ResetLevel();
    }

    #endregion

    #region Knockback

    private IEnumerator Invicibility()
    {
        Color origin = sprite.color;
        Color derkenColor = new Color(sprite.color.r, sprite.color.b, sprite.color.g, .5f);

        canBeKnockback = false;

        for (int i = 0; i < 3; i++)
        {
            sprite.color = derkenColor;

            yield return new WaitForSeconds(.1f);

            sprite.color = origin;
        }

        canBeKnockback = true;
    }


    private void Knockback()
    {
        if (!canBeKnockback) return;

        StartCoroutine(Invicibility());
        isKnocked = true;
        rb.velocity = knockbackDirection;
    }

    public void KnockbackAnimationEvent() => isKnocked = false;

    #endregion

    #region Roll ability
    public void RollAnimationEvent() => animator.SetBool("canRoll", false);
    #endregion

    #region Speed Controller

    private void SpeedReset()
    {
        moveSpeed = defaultSpeed;
        milestoneIncreaser = defaultMilestoneIncreaser;
    }

    private void SpeedController()
    {
        if (moveSpeed == maxSpeed) return;

        if (transform.position.x > speedMileStone)
        {
            speedMileStone += milestoneIncreaser;

            moveSpeed *= speedMuitiple;
            milestoneIncreaser *= speedMuitiple;

            if (moveSpeed > maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }
    }

    #endregion

    #region LedgeClimb ability

    private void CheckForLedge()
    {
        if (ledgeDetected && canGrabLedge)
        {
            canGrabLedge = false;

            Vector2 ledgePos = GetComponentInChildren<LedgeDetection>().transform.position;

            rb.gravityScale = 0f;

            climbBegunPosition = ledgePos + offset1;
            climbOverPosition = ledgePos + offset2;

            canClimb = true;
        }

        if (canClimb)
        {
            transform.position = climbBegunPosition;
        }
    }

    public void LedgeClimbOver()
    {
        canClimb = false;
        transform.position = climbOverPosition;

        rb.gravityScale = 5f;

        Invoke(nameof(AllowLedgeGrab), .1f);
    }

    private void AllowLedgeGrab()
    {
        canGrabLedge = true;
    }
    #endregion

    #region Slide ability
    private void CheckForSlide()
    {
        if (slideTimeCounter < 0 && !ceilingDetected)
        {
            isSliding = false;
        }

    }

    private void SlideButton()
    {
        if (rb.velocity.x != 0 && slideCooldownCounter < 0)
        {
            isSliding = true;
            slideTimeCounter = slideTime;
            slideCooldownCounter = slideCooldown;
        }
    }
    #endregion

    #region Movement
    private void Movement()
    {
        if (wallDetected)
        {
            SpeedReset();
        }

        if (isSliding)
        {
            rb.velocity = new Vector2(slideSpeed, 0);
        }
        else
            rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
    }
    #endregion

    #region Jump ability
    private void JumpButton()
    {
        if (isSliding) return;

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
    #endregion
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + ceilingcheckDisctance));
        Gizmos.DrawWireSphere(checkGround.position, checkDisctance);
        Gizmos.DrawWireCube(checkWall.position, wallCheckSize);
    }
}
