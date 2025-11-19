using UnityEngine;

public class FoxMovements : MonoBehaviour
{
    
    [SerializeField] private float speed = 1f;

    public Rigidbody2D playerBody;
    public PlaytimeKeyBind pausedGameManager;
    public Player player;

    private float horizontal, vertical;

    private Camera cam;
    private Vector3 mousePosition;
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pausedGameManager.IsPaused())
        {
            horizontal = 0;
            vertical = 0;
            if (Input.GetKey(KeyCode.A)) //left;  
            {
                horizontal = -1;
            }
            if (Input.GetKey(KeyCode.D)) //right;
            {
                horizontal = 1;
            }
            if (Input.GetKey(KeyCode.W)) //up;
            {
                vertical = 1;
            }
            if (Input.GetKey(KeyCode.S)) //down;
            {
                vertical = -1;
            }
            // maybe add jump
            //TODO: add attack, spells
            Vector3 tempVect = new Vector3(horizontal, vertical, 0);
            tempVect = tempVect.normalized * speed * Time.deltaTime;

            playerBody.MovePosition(playerBody.transform.position + tempVect);

            if (Input.GetMouseButtonUp(0)) // 0 for left click, 1 for right, 2 for middle
            {
                player.attack(mousePosition);
                //OnGUI();
            }

        }
    }


    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        mousePosition = point;
       /* GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
       */
    }
}
