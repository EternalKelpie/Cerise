using UnityEngine;

public class ShadowBehaviour : BaseOponnentScript
{

    public GameObject shadowFire;

    GameObject clone;
    float refreshTime = 3f;
    float timer = 0;

    public bool Init( float speedIn ,
         float canSeeDistanceIn, 
        float maxHpIn,
        PlaytimeKeyBind pausedGameManagerIn,
         Map gridManagerIn,
         GameObject playerObject,
         Player playerIn)
    {
        
            speed = speedIn;
            canSeeDistance = canSeeDistanceIn;
            maxHp = maxHpIn;
            pausedGameManager = pausedGameManagerIn;
            gridManager = gridManagerIn;
            playerPathfindingPurpouse = playerObject;
            player = playerIn;
        
        healthBar.CreateHealthBar(maxHp / maxHp);
        _hp = maxHp;
        healthBar.UpdateHealthBar(maxHp / maxHp);
        def = 10;
        attackValue = 10;
        corruptionPoints = 15;


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
        timer += Time.unscaledDeltaTime;
        //timerForAttack += Time.unscaledDeltaTime;
        base.FixedUpdate();
        if (attacking && timer >= refreshTime)
        {
            timer = 0f;
            attack();
        }
    }

    void attack()
    {
        Vector3 direction = (playerPathfindingPurpouse.transform.position - transform.position).normalized;

        clone = Instantiate(shadowFire, transform.position, Quaternion.identity);
        clone.GetComponent<ShadowAttack>().Init(this, direction);

    }


}
