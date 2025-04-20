using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 3f;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    public enum WalkableDirection
    {
        Left,
        Right
    }
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector;
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
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall){
            flipDirection();
        }
        rb.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.linearVelocity.y);
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
}
