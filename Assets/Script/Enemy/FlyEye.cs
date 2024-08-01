using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyEye : MonoBehaviour
{
    protected bool isTravelingToEndPoint;
    protected Vector2 startPoint = Vector2.zero;
    protected Vector2 endPoint = Vector2.zero;

    [SerializeField] protected float speed = 1f;
    [SerializeField] protected Vector2 diffVector = new Vector2(2, 0);
    
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;
    
    [SerializeField] protected DetectionZone attackZone;
    [SerializeField] protected Damageable damageable;

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
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        startPoint = transform.position;
        endPoint = startPoint + diffVector;
    }

    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if(AttackCooldown > 0) AttackCooldown -= Time.deltaTime;
        Moving();
        UpdateDirection();
    }

    protected void Moving()
    {
        if (!damageable.IsAlive) return;
        if (!CanMove)
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }

        Vector2 targetWayPoint = isTravelingToEndPoint  ? endPoint : startPoint;
        Vector2 direction = (targetWayPoint - new Vector2(transform.position.x, transform.position.y)).normalized;

        rb.velocity = direction * speed;
        
        float distance = Vector2.Distance(targetWayPoint, transform.position);
        if (distance < 0.01f)
        {
            isTravelingToEndPoint = isTravelingToEndPoint ? false : true;
        }
    }

    protected void UpdateDirection()
    {
        //If nothing change (both look in the same direction)
        if (transform.localScale.x * rb.velocity.x >= 0) return;
        
        Vector3 locScale = transform.localScale;
        transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
    }
}
