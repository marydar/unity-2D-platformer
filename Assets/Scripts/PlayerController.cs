using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections), typeof(Damagable))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpInpulse = 10f;
    TouchingDirections touchingDirections;
    Damagable damagable;

    public float CurrentMoveSpeed {
        get{
            if(CanMove){
                if(IsMoving && !touchingDirections.IsOnWall){
                    if(touchingDirections.IsGrounded){
                        if(IsRunning){
                            return runSpeed;
                        }
                        return walkSpeed;
                    }   
                    return airWalkSpeed;
                }
                //Idle speed
                return 0;
            }
            return 0;
        }
    }
    
    Vector2 moveInput;

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving 
    { 
        get
        {
            return _isMoving;
        } 
        set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        } 
    }
    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning {
        get
        {
            return _isRunning;
        } 
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        } 
    }
    public bool _isFacingRight = true;

    public bool IsFacingRight{
        get 
        {
            return _isFacingRight;
        } 
        private set 
        {
            if(value != _isFacingRight){
                transform.localScale *= new Vector2 (-1, 1);
            }
            _isFacingRight = value;
        }
    }
    public bool CanMove{
        get{
            return animator.GetBool(AnimationStrings.canMove);
        }
    }
    public bool IsAlive{
        get{
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    Rigidbody2D rb;
    Animator animator;
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
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(!damagable.LockVelocity){
            rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed , rb.linearVelocity.y);
        }
        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocity.y);
        // transform.position += rb.velocity * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive){
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else{
            IsMoving = false;
        }
    }
    private void SetFacingDirection(Vector2 moveInput){
        if(moveInput.x > 0 && !IsFacingRight){
            //face the right
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight){
            //face the left
            IsFacingRight = false;
        }
    }
    public void OnRun(InputAction.CallbackContext context){
        if(context.started){
            IsRunning = true;
        }
        else if(context.canceled){
            IsRunning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context){
        if(context.started && touchingDirections.IsGrounded && CanMove){
            animator.SetTrigger(AnimationStrings.jump);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpInpulse);
        }
    }
    public void OnAttack(InputAction.CallbackContext context){
        if(context.started){
            animator.SetTrigger(AnimationStrings.attack);
        }
    }
    public void OnHit(int damage, Vector2 knockBack){
        rb.linearVelocity = new Vector2(knockBack.x, rb.linearVelocity.y+knockBack.y);
    }
}
