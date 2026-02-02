using UnityEngine;
using System.Collections;
using System.Collections.Generic; 


public class WeaponAttackCollisionDetection : MonoBehaviour
{
    float refreshTimeForAttack = 4f;
    float timerForAttack = 0;
    public BaseOponnentScript opponent;
    public GameObject opponentPosition;

    [SerializeField] Vector3 offset;

    ShieldAbility shield;

    private void Start()
    {
        StartCoroutine(UpdatePosition());
        if (offset == null)
        {
            new Vector3(0.3f, 0f, 0f);
        }
        shield = FindObjectOfType<ShieldAbility>();

    }

    void Update()
    {
        timerForAttack += Time.unscaledDeltaTime;

    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (shield.IsShieldActive)
            return;
       

        Player Foxopponent = collision.gameObject.GetComponent<Player>();

        if (Foxopponent != null && timerForAttack >= refreshTimeForAttack)
        {
            timerForAttack += Time.unscaledDeltaTime;
            Foxopponent.GetDamage(opponent.getAttackValue());
           

        }


    }

   IEnumerator UpdatePosition()
   {
        while (true)
        {
            transform.position = opponentPosition.transform.position + offset;
            yield return null; 
        }
   }





}
