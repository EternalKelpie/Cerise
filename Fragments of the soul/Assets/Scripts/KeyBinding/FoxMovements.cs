using UnityEngine;

public class FoxMovements : MonoBehaviour
{
    
    [SerializeField] private float speed = 1f;
    [SerializeField] private float cameraScrollIn = 4f;

    public Rigidbody2D playerBody;
    public PlaytimeKeyBind pausedGameManager;
    public Player player;
    public Animator animator;
    public ShieldAbility shield;

    float horizontal, vertical;

    Camera cam;
    Vector3 mousePosition;
    float attackCooldown = 0.4f; 
    float lastAttackTime = -1f;

    float refreshTimeSpecialMove = 3f;
    float timerSpecialMove = 0;

    void Start()
    {
        cam = Camera.main;
        cam.orthographicSize = cameraScrollIn;
    }

    // Update is called once per frame
    void Update()
    {
        timerSpecialMove += Time.unscaledDeltaTime;
        if (!pausedGameManager.IsPaused())
        {
            horizontal = 0;
            vertical = 0;
            if (Input.GetKey(KeyCode.A)) //left;  
            {
                horizontal = -1;
                animator.SetBool("WalksRight", false);
                animator.SetBool("Stands", false);
                animator.SetBool("WalksLeft", true);
            }
            else if (Input.GetKey(KeyCode.D)) //right;
            {
                horizontal = 1;
                animator.SetBool("WalksLeft", false);
                animator.SetBool("Stands", false);
                animator.SetBool("WalksRight", true);
            }
            if (Input.GetKey(KeyCode.W)) //up;
            {
                vertical = 1;
                
            }
            else if (Input.GetKey(KeyCode.S)) //down;
            {
                vertical = -1;
            
            }
            // maybe add jump
            //TODO: add attack, spells
            Vector3 tempVect = new Vector3(horizontal, vertical, 0);
            tempVect = tempVect.normalized * speed * Time.deltaTime;

            if (tempVect == Vector3.zero)
            {
                animator.SetBool("Stands", true);
                animator.SetBool("WalksRight", false);
                animator.SetBool("WalksLeft", false);
            }

            playerBody.MovePosition(playerBody.transform.position + tempVect);
            cam.transform.position = Vector3.Lerp( cam.transform.position, playerBody.transform.position + new Vector3(0, 0, -10), 0.1f);


            /*if (Input.GetMouseButtonUp(0)) // 0 for left click, 1 for right, 2 for middle
            {
                player.attack(mousePosition);
                //OnGUI();
            }*/
            if (Input.GetMouseButtonUp(0))
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                    Input.mousePosition.x,
                    Input.mousePosition.y,
                    -Camera.main.transform.position.z
                ));


                    player.attack(mouseWorldPosition, playerBody.position);
                    lastAttackTime = Time.time;
                }
            }
           


            if (Input.GetKey(KeyCode.Q) && timerSpecialMove >= refreshTimeSpecialMove && player.circleAttackAcquired)
            {
                player.CircleAttack();
                timerSpecialMove = 0f;

            }
            if (Input.GetKey(KeyCode.Q) && timerSpecialMove >= refreshTimeSpecialMove && player.shieldAcquired)
            {
                shield.ActivateShield();
                timerSpecialMove = 0f;

            }

        }
    }


    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        mousePosition = point;
    }
}
