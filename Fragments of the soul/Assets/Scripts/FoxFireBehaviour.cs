using System.ComponentModel;
using UnityEngine;

public class FoxFireBehaviour : MonoBehaviour
{

     Player player;
    public void Init(Player shooter)
    {
        player = shooter;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        BaseOponnentScript opponent = collision.gameObject.GetComponent<BaseOponnentScript>();

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

