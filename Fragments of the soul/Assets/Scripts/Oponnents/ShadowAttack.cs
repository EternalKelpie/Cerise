using UnityEngine;

public class ShadowAttack : MonoBehaviour
{

    float lifetimeCounter = 0f;
    float lifetime = 4f;
    Vector3 Attackdirection;
    ShadowBehaviour shadow;
    Rigidbody2D rb;
    float speed = 2.5f;
    bool isInitialized = false;


    public void Init(ShadowBehaviour shadowMonster, Vector3 direction)
    {
        shadow = shadowMonster;
        Attackdirection = direction;
 
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Attackdirection * speed; 
        }
        isInitialized = true;
    }

    public void Update()
    {

        lifetimeCounter += Time.deltaTime;

        if (lifetimeCounter >= lifetime)  // how long it should be alive
        {
            Destroy(gameObject);
        }


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        Player playerCollision = collision.gameObject.GetComponent<Player>();

        if (playerCollision != null)
        {
            playerCollision.GetDamage(shadow.getAttackValue());
            Destroy(gameObject);

            return;
        }

        BaseOponnentScript opponent = collision.gameObject.GetComponent<BaseOponnentScript>();
        if (opponent == null)
        {
            Destroy(gameObject);



        }


    }


}








