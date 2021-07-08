using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    public enum MovementStates
    {
        Idle,
        Running,
        Jumping,
        Attacking
    }
    public enum FacingDirection
    {
        Right,
        Left
    }
    [Header("Raycast lenght and layerMask")]
    public float isGroundedRayLegnght;
    public LayerMask platformLayerMask;

    [Header("Movement Values")]
    public float moveSpeed = 0.5f;
    public float jumpHeight = 0.5f;

    [Header("Movement States")]
    public MovementStates characterMovementState;
    public FacingDirection facingDirection;

    private Rigidbody2D rigidBody2D;
    private CharacterAnimationController animController;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animController = GetComponent<CharacterAnimationController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {

       


    }
    /// <summary>
    /// This method handles jumping
    /// </summary>



    private void FixedUpdate()
    {
        HandleMovement();
        IsGrounded();
        SetCharachterState();
        //PlayAnimationsBasedOnState();
        SetCharacterD›rection();
        HandleJump();

    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.UpArrow) && IsGrounded())
        {

            
            rigidBody2D.velocity = Vector2.up * jumpHeight;



        }

    }


    private void HandleMovement()
    {
        rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (Input.GetKey(KeyCode.LeftArrow))
        {


            rigidBody2D.velocity = new Vector2(-moveSpeed, rigidBody2D.velocity.y);
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {


                rigidBody2D.velocity = new Vector2(moveSpeed, rigidBody2D.velocity.y);
            }

            else // NO KEYS PRESSED
            {

                rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
                rigidBody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    /// <summary>
    /// This method controls are the character is grounding or not grounding
    /// </summary>
    private bool IsGrounded()
    {



        RaycastHit2D raycastHit2D = Physics2D.BoxCast(spriteRenderer.bounds.center,
            spriteRenderer.bounds.size, 0f,
            Vector2.down, isGroundedRayLegnght, platformLayerMask);

        Color rayColor;

        if (raycastHit2D.collider != null)
        {
            rayColor = Color.green;

        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(spriteRenderer.bounds.center + new Vector3(spriteRenderer.bounds.extents.x, 0),
        Vector2.down * (spriteRenderer.bounds.extents.y + isGroundedRayLegnght), rayColor);
        Debug.DrawRay(spriteRenderer.bounds.center + new Vector3(0, spriteRenderer.bounds.extents.y),
        Vector2.down * (spriteRenderer.bounds.extents.y + isGroundedRayLegnght), rayColor);
        if (raycastHit2D.collider != null)
        {

            Debug.Log("We are colliding with " + raycastHit2D.collider);
        }

        return raycastHit2D.collider != null;
    }

    /// <summary>
    /// This method sets character movement states
    /// </summary>
    private void SetCharachterState()
    {

        if (IsGrounded())
        {
            if (rigidBody2D.velocity.x == 0)
            {
                animController.PlayIdleAnim();

            }
            else if (rigidBody2D.velocity.x > 0)
            {
                facingDirection = FacingDirection.Right;
                animController.PlayRunningAnim();


            }
            else if (rigidBody2D.velocity.x < 0)
            {
                facingDirection = FacingDirection.Left;
                animController.PlayRunningAnim();
            }
        }
        else
        {
            animController.PlayJumpingAnim();
        }
    }

    private void SetCharacterD›rection()
    {
        switch (facingDirection)
        {
            case FacingDirection.Right:
                spriteRenderer.flipX = false;
                break;
            case FacingDirection.Left:
                spriteRenderer.flipX = true;
                break;
        }

    }

    private void PlayAnimationsBasedOnState()
    {
        switch (characterMovementState)
        {
            case MovementStates.Idle:
                animController.PlayIdleAnim();
                break;
            case MovementStates.Running:
                animController.PlayRunningAnim();
                break;
            case MovementStates.Jumping:
                animController.PlayJumpingAnim();
                break;
            case MovementStates.Attacking:
                break;
            default:
                break;
        }


    }
}
