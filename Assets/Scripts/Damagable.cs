using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damagableHit;
    Animator animator;
    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth {
        get{
            return _maxHealth;
        }
        set{
            _maxHealth = value;
            
        }
    }
    [SerializeField]
    private int _health = 100;
    public int Health {
        get{
            return _health;
        }
        set{
            _health = value;
            if (value <= 0){
                IsAlive = false;
            }
        }
    }
    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive {
        get{
            return _isAlive;
        }
        set{
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("isAlive: " + value);
        }
    }
    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    public  float invincibilityTime = 0.25f;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public bool LockVelocity {
        get{
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set{
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }
    public bool Hit(int damage, Vector2 knockBack){
        if (IsAlive && !isInvincible){
            Health -= damage;
            isInvincible = true;
            animator.SetTrigger(AnimationStrings.hit);
            LockVelocity = true;
            damagableHit?.Invoke(damage, knockBack);
            CharacterEvents.characterDamaged?.Invoke(gameObject, damage);
            return true;
        }
        return false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInvincible){
            if(timeSinceHit > invincibilityTime){
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }
}
