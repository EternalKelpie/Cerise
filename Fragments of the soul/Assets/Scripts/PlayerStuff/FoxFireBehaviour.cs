using System.ComponentModel;
using UnityEngine;

public class FoxFireBehaviour : MonoBehaviour
{
    float lifetimeCounter = 0f;
    float lifetime = 4f;

    Player player;
    public void Init(Player shooter)
    {
        player = shooter;
    }

    public void Update()
    {
        lifetimeCounter += Time.deltaTime;

        if (lifetimeCounter >= lifetime)  // how long it should be alive
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        BaseOponnentScript opponent = collision.GetComponent<BaseOponnentScript>();

        if (opponent != null)
        {
            opponent.TakeDamage(player.getAttackValue());
            Destroy(gameObject);
            return;
        }

        Player playerCollision = collision.gameObject.GetComponent<Player>();
            if (playerCollision == null)
            {
                Destroy(gameObject);

            }

    }



}

