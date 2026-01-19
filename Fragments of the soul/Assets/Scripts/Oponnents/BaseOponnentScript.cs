using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BaseOponnentScript : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float canSeeDistance = 10f;  //Todo: dodaæ widocznoœæ, sprawdzanie, ¿eby przez œciany nie by³o widaæ
    [SerializeField] protected float maxHp =50;

    public PlaytimeKeyBind pausedGameManager;
    //Todo: hp, attack, defense

    public Rigidbody2D oponnent;
    public GameObject playerPathfindingPurpouse;
    public Player player;
    public Map gridManager;
    public HealthBarBehaviour healthBar;
    public string enemyID;
    public bool isLoading = false;



    // protected NavMeshAgent agent;
    protected float horizontal, vertical;
    protected bool attacking;

    
    public Pathfinding pathfinder;
    //public Sprite healthBarSprite;

    protected List<Vector2Int> path;
    protected int pathIndex;

    protected float _hp;
    protected float def, corruptionPoints, attackValue;


    private Vector2Int lastPlayerPos;
    private float pathUpdateInterval = 0.5f; // Aktualizuj œcie¿kê co 0.5s
    private float lastPathUpdateTime = 0f;



    /* protected GameObject healthBar;
     protected GameObject healthBarFill;
     protected Vector3 healthBarOffset = new Vector3(0, 1f, 0);
    */
    public float hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            if (isLoading && _hp <= 0)
            {
                Destroy(gameObject);
            }
            else if (_hp<0)
            {
                // die
                player.addCorruptionPoints(corruptionPoints);
                if (player.healAfterKillAcquired)
                {
                    player.HealSlightly();
                }
                Destroy(gameObject);
               
            }
            if (healthBar != null)
            {
                healthBar.UpdateHealthBar(hp / maxHp);
            }
        }
    }


    public bool Init()
    {
        healthBar.CreateHealthBar(maxHp / maxHp);
        _hp = maxHp;
        healthBar.UpdateHealthBar(maxHp / maxHp);
        def = 0;
        attackValue = 5;
        corruptionPoints = 5;

        return true;

    }



// Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
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
        }



    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (!pausedGameManager.IsPaused())
        {
            if (!attacking)
            {
                CheckDistance();
                // Roam();
            }
            else
            {

                if (Time.time - lastPathUpdateTime > pathUpdateInterval)
                {
                    UpdateOponentPath();
                    lastPathUpdateTime = Time.time;
                }

                MoveAlongPath();
                /*if (path == null || pathIndex >= path.Count)
                {
                    if (path == null)
                    {
                        Vector2Int start = WorldToGrid(oponnent.transform.position);
                        Vector2Int target = WorldToGrid(playerPathfindingPurpouse.transform.position);
                        path = pathfinder.FindPath(start, target);
                        pathIndex = 0;
                    }
                    return;
                }

                Vector3 targetPos = GridToWorld(path[pathIndex]);
                oponnent.transform.position = Vector3.MoveTowards(oponnent.transform.position, targetPos, speed * Time.deltaTime);


                if (Vector3.Distance(oponnent.transform.position, targetPos) < 0.1f)
                {
                    pathIndex++;
                }*/

            }
        }
    }
    private void UpdateOponentPath()
    {
        Vector2Int currentPlayerPos = WorldToGrid(playerPathfindingPurpouse.transform.position);

        
        if (currentPlayerPos != lastPlayerPos)
        {
            Vector2Int start = WorldToGrid(oponnent.transform.position);
            path = pathfinder.FindPath(start, currentPlayerPos);
            pathIndex = 0;
            lastPlayerPos = currentPlayerPos;
        }
    }

    private void MoveAlongPath()
    {
        if (path == null || pathIndex >= path.Count)
        {
            path = null; 
            return;
        }

        Vector3 targetPos = GridToWorld(path[pathIndex]);
        oponnent.transform.position = Vector3.MoveTowards(oponnent.transform.position, targetPos, speed * Time.deltaTime);


        if (Vector3.Distance(oponnent.transform.position, targetPos) < 0.1f)
        {
            pathIndex++;
        }
    }

    public void TakeDamage(float value)
    {

        Debug.Log(" damage taken: " +value);
        Debug.Log(" hp before: " + hp);
        hp -= value - (0.01f * def * value );
        Debug.Log(" hp after: " + hp);

    }



    protected IEnumerator UpdatePath()
    {
        while (true)
        {
            if (!pausedGameManager.IsPaused())
                if (gridManager != null && playerPathfindingPurpouse != null)
                {
                    Vector2Int start = WorldToGrid(oponnent.transform.position);
                    Vector2Int target = WorldToGrid(playerPathfindingPurpouse.transform.position);
                    path = pathfinder.FindPath(start, target);
                    pathIndex = 0;
                    yield return new WaitForSeconds(1f);
                }
        }
    }

    protected void CheckDistance() 
    {
        if (Vector3.Distance(oponnent.transform.position, playerPathfindingPurpouse.transform.position) <= canSeeDistance)
        {
            attacking = true;
        }
    }

    protected Vector2Int WorldToGrid(Vector3 worldPos)
    {
        Vector3Int cell = gridManager.walkableMap.WorldToCell(worldPos);
        return new Vector2Int(cell.x, cell.y);
    }


    protected Vector3 GridToWorld(Vector2Int gridPos)
    {
        Vector3 world = gridManager.walkableMap.CellToWorld(new Vector3Int(gridPos.x, gridPos.y, 0));
        return world+ new Vector3(0.1f, 0.1f, 0f);

    }

    protected void attack()
    {
        return;
    }

    public float getAttackValue()
    {
        return attackValue;
    }

 

}
