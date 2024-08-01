using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    protected Vector2 moveInput;
    [SerializeField] protected float walkSpeed = 1f;
    [SerializeField] protected float runSpeed = 2f;
    [SerializeField] protected float jumpForce = 4.5f;
    
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;
    [SerializeField] protected TouchingDirection touchingDirection;
    [SerializeField] protected Damageable damageable;
    protected float MoveSpeed
    {
        get
        {
            if(!CanMove) return 0;
            if (!_isMoving) return 0;
            if (touchingDirection.IsOnWall) return 0;
            
            if (_isRuning) return runSpeed;
            return walkSpeed;
        }
    }

    [SerializeField] protected bool _isMoving = false;
    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationString.isMoving, value);
        }
    }
    
    [SerializeField] protected bool _isRuning = false;
    public bool IsRuning
    {
        get { return _isRuning; }
        private set
        {
            _isRuning = value;
            animator.SetBool(AnimationString.isRuning, value);
        }
    }

    protected bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                // Flip
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool(AnimationString.canMove); }
    }
    
    public bool IsAlive
    {
        get { return animator.GetBool(AnimationString.isAlive); }
    }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent <TouchingDirection>();
        damageable = GetComponent<Damageable>();
    }
    

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!IsAlive)
        {
            IsMoving = false;
            return;
        }
        
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }
    
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRuning = true;
        }
        else if (context.canceled)
        {
            IsRuning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirection.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationString.jump);
            Jumping();
        }
    }
    
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
               animator.SetTrigger(AnimationString.attack);
        }
    }
    
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationString.rangedAttack);
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    protected void Moving()
    {
        if (damageable.LockVelocity) return;
        
        rb.velocity = new Vector2(moveInput.x * MoveSpeed, rb.velocity.y);
        
        animator.SetFloat(AnimationString.yVelocity, rb.velocity.y);
    }

    protected void Jumping()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

}
