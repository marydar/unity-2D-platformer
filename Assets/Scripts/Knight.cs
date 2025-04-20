using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damagable))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 3f;
    public DetectionZone attackZone;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damagable damagable;
    public enum WalkableDirection
    {
        Left,
        Right
    }
     public bool CanMove{
        get{
            return animator.GetBool(AnimationStrings.canMove);
        }
    }
    private WalkableDirection _walkDirection = WalkableDirection.Right;
    private Vector2 walkDirectionVector = Vector2.right;
    public WalkableDirection WalkDirection{
        get{
            return _walkDirection;
            }
        set{
            if (value != _walkDirection){
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkableDirection.Right){
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left){
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }
    public bool _hasTarget = false;
    public bool HasTarget{
        get{
            return _hasTarget;
        }
        set{
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damagable = GetComponent<Damagable>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {   
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall){
            flipDirection();
        }
        if(!damagable.LockVelocity){
            if(CanMove && !HasTarget){
            rb.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.linearVelocity.y);
            }
            else{
                rb.linearVelocity = Vector2.zero;
            }
        }
    }
    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }
    private void flipDirection(){
        if(WalkDirection == WalkableDirection.Right){
            WalkDirection = WalkableDirection.Left;
        }
        else if(WalkDirection == WalkableDirection.Left){
            WalkDirection = WalkableDirection.Right;
        }
        else {
            Debug.LogError("BITCH");
        }
    }
    public void OnHit(int damage, Vector2 knockBack){
        rb.linearVelocity = new Vector2(knockBack.x, rb.linearVelocity.y+knockBack.y);
    }
}
