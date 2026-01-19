using UnityEngine;

public class AnimatorEnemy : MonoBehaviour
{
    private Animator anim;
    private Vector3 lastPos;
    private bool facingRight = true;
    private bool wasMoving = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        lastPos = transform.position;
    }

    void Update()
    {
        Vector3 currentPos = transform.position;
        bool isMoving = Mathf.Abs(currentPos.x - lastPos.x) > 0.001f; 

        if (isMoving)
        {
            bool movingRight = currentPos.x > lastPos.x;

            
            if (movingRight != facingRight)
            {
                facingRight = movingRight;
                anim.SetBool("FacingRight", facingRight);
                anim.SetTrigger("Turn"); 
            }
        }

        if (isMoving != wasMoving)
        {
            anim.SetBool("IsMoving", isMoving);
            wasMoving = isMoving;
        }

        lastPos = currentPos;
    }
}