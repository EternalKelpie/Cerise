using UnityEngine;

public class WollfBehaviour : BaseOponnentScript
{


    public bool Init()
    {
        healthBar.CreateHealthBar(maxHp / maxHp);
        if (!isLoading)
        {
            _hp = maxHp;
        }
        healthBar.UpdateHealthBar(maxHp / maxHp);
        def = 5;
        attackValue = 5;
        corruptionPoints = 5;

        return true;

    }

    void Start()
    {
        Init();
        attacking = false;
        pathfinder = GetComponent<Pathfinding>();
        if (pathfinder == null)
        {
            pathfinder = gameObject.AddComponent<Pathfinding>();
        }
        if (gridManager != null)
        {
            pathfinder.Initialize(gridManager);
        }
        StartCoroutine(UpdatePath());
        oponnent.freezeRotation = true;
    }

    void Update()
    {
        //timerForAttack += Time.unscaledDeltaTime;
        base.Update();
    }

    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("uderzony 1 ");

            Player Foxopponent = collision.gameObject.GetComponent<Player>();

            if (Foxopponent != null && timerForAttack >= refreshTimeForAttack)
            {
                timerForAttack += Time.unscaledDeltaTime;
                Debug.Log("uderzony 2 ");
                player.GetDamage(attackValue);

            }

        
    }*/

   




}
