using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField] protected float walkSpeed = 1f;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;

    [SerializeField] protected TouchingDirection touchingDirection;
    [SerializeField] protected DetectionZone attackZone;
    [SerializeField] protected DetectionZone cliffDetectionZone;
    [SerializeField] protected Damageable damageable;
    public enum WalkableDirection {Right, Left}

    protected Vector2 walkDirectionVector = Vector2.right;
    protected WalkableDirection _walkDirection;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        private set
        {
            if (_walkDirection != value)
            {
                // Flip
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    protected bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationString.hasTarget, value);
        }
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        }
    }
    
    public float AttackCooldown
    {
        get { return animator.GetFloat(AnimationString.attackCooldown); }
        private set
        {
            animator.SetFloat(AnimationString.attackCooldown, Mathf.Max(value, 0f));
        }
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if(AttackCooldown > 0) AttackCooldown -= Time.deltaTime;
        
        if (touchingDirection.IsGrounded && touchingDirection.IsOnWall)
        {
            FlipDirection();
        }
        Moving();
    }

    protected void Moving()
    {
        if (damageable.LockVelocity) return;
        if (!CanMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }
        rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
    }

    protected void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if (touchingDirection.IsGrounded)
        {
            FlipDirection();
        }
    }
}
