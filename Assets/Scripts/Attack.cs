using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage = 10;
    public Vector2 knockBack = Vector2.zero;
    void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision){
        Damagable damagable = collision.GetComponent<Damagable>();
        if(damagable != null){
            Vector2 delieveredKnockBack = transform.parent.localScale.x > 0 ? knockBack : new Vector2(knockBack.x * -1, knockBack.y);
            bool gotHit = damagable.Hit(attackDamage, delieveredKnockBack);
            if(gotHit){
                Debug.Log(collision.name + " hit for " + attackDamage);
            }
            
        }
    }
}
