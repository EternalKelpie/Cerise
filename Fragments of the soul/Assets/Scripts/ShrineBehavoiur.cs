using UnityEngine;
using UnityEngine.UI;


public class ShrineBehavoiur : MonoBehaviour
{
    public SpriteRenderer useHealingPopup;
    public SpriteRenderer clensePopup;
    public Player player;
    public PlaytimeKeyBind keyBind;

    float canInteractDistance = 3f;
    bool canHeal, canClense, canSee;
    bool healUsed, cleanseUsed;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        useHealingPopup.enabled = false;
        clensePopup.enabled = false;
        canSee = false;
        canHeal = true;
        canClense = true;
        healUsed = false;
        cleanseUsed = false;


    }

    void Update()
    {
        CheckDistance();
        if (!keyBind.IsPaused() && canSee)
        {
            if (Input.GetKey(KeyCode.E) && canHeal && !healUsed)  
            {
                player.Heal();
                healUsed = true;
            }
            if (Input.GetKey(KeyCode.R) && canClense && !cleanseUsed) 
            {
                player.Cleanse();
                cleanseUsed = true;
            }


        }
    }


    protected void CheckDistance()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= canInteractDistance)
        {
            if (!cleanseUsed) 
            {
                clensePopup.enabled = true;
            }
            else
            {
                clensePopup.enabled = false;
            }
            if (!healUsed)
            {
                useHealingPopup.enabled = true;
            }
            else
            {
                useHealingPopup.enabled = false;
            }
            canSee = true;
        }
        else 
        {
            useHealingPopup.enabled = false;
            clensePopup.enabled = false;
            canSee = false;
        }

    }
}
