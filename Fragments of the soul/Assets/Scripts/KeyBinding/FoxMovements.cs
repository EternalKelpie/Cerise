using UnityEngine;

public class FoxMovements : MonoBehaviour
{
    
    [SerializeField] private float speed = 1f;

    public Rigidbody2D player;

    private float horizontal, vertical;


    // Update is called once per frame
    void Update()
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

        player.MovePosition(player.transform.position + tempVect);
    }
}
