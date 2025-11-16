using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BaseOponnentScript : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float canSeeDistance = 10f;  //Todo: dodaæ widocznoœæ, sprawdzanie, ¿eby przez œciany nie by³o widaæ
    [SerializeField] float maxHp =50;

    public PlaytimeKeyBind pausedGameManager;
    //Todo: hp, attack, defense

    public Rigidbody2D oponnent;
    public GameObject player;
    public Map gridManager;
    public HealthBarBehaviour healthBar;


    // protected NavMeshAgent agent;
    protected float horizontal, vertical;
    protected bool attacking;

    
    public Pathfinding pathfinder;
    //public Sprite healthBarSprite;

    protected List<Vector2Int> path;
    protected int pathIndex;

    protected float _hp;



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
            if (_hp<0)
            {
                // die
            }
            if (healthBar != null)
            {
                //healthBar.UpdateHealthBar(hp / maxHp);
            }
        }
    }


    public bool Init()
    {
        healthBar.CreateHealthBar(maxHp / maxHp);
        _hp = maxHp;
        healthBar.UpdateHealthBar(maxHp / maxHp);

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

    IEnumerator UpdatePath()
    {
        while (true)
        {
            if(!pausedGameManager.IsPaused())
            if (gridManager != null && player != null)
            {
                Vector2Int start = WorldToGrid(oponnent.transform.position);
                Vector2Int target = WorldToGrid(player.transform.position);
                path = pathfinder.FindPath(start, target);
                pathIndex = 0;
                yield return new WaitForSeconds(1f);
            }
        }
    }

    // Update is called once per frame
    protected void Update()
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

                if (path == null || pathIndex >= path.Count)
                {
                    if (path == null)
                    {
                        Vector2Int start = WorldToGrid(oponnent.transform.position);
                        Vector2Int target = WorldToGrid(player.transform.position);
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
                }

            }
        }
    }

    protected void CheckDistance() 
    {
        if (Vector3.Distance(oponnent.transform.position, player.transform.position) <= canSeeDistance)
        {
            attacking = true;
        }
    }

    protected Vector2Int WorldToGrid(Vector3 worldPos)
    {
        Vector3Int cell = gridManager.walkableMap.WorldToCell(worldPos);
        return new Vector2Int(cell.x, cell.y);
    }

   /* protected Vector3 GridToWorld(Vector2Int gridPos)
    {
        Vector3 world = gridManager.walkableMap.CellToWorld(new Vector3Int(gridPos.x, gridPos.y, 0));
        return world + new Vector3(0.5f, 0.5f, 0f); // center of tile
    }*/
    protected Vector3 GridToWorld(Vector2Int gridPos)
    {
        Vector3 world = gridManager.walkableMap.CellToWorld(new Vector3Int(gridPos.x, gridPos.y, 0));
        return world + new Vector3(0.5f, 0.25f, 0f);

    }

    protected void attack()
    { 
        
    }

    /*void CreateHealthBar()
    {
        healthBar = new GameObject("Healthbar");
        healthBar.transform.SetParent(transform);
        healthBar.transform.localPosition = healthBarOffset;

        healthBarFill = new GameObject("Fill");
        healthBarFill.transform.SetParent(healthBar.transform);
        healthBar.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
        var fillRenderer = healthBarFill.AddComponent<SpriteRenderer>();
        fillRenderer.sprite = healthBarSprite;
        fillRenderer.color = Color.green;
        fillRenderer.sortingOrder = 1;

        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBar == null)
        {
            return;
        }

        float healthPercentage = hp / maxHp;
        healthBarFill.transform.localScale = new Vector3(healthPercentage, 1f, 1);


        var healthBarRenderer = healthBarFill.GetComponent<SpriteRenderer>();
        healthBarRenderer.color = Color.Lerp(Color.red, Color.green, healthPercentage);


        if (hp<0)
        {
            healthBar.SetActive(false);
        }
    }*/

}
