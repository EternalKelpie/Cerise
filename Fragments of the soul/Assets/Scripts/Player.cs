using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerHp healthbar;
    public float currentHealth, maxhealth;



    void Start()
    {
        healthbar.SetMaxHealth(maxhealth);
    }


    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            SetHeath(20f);
        }
        if (Input.GetKeyDown("l"))
        {
            SetHeath(-20f);
        }


    }

    public void SetHeath(float healthChange)
    { 
        currentHealth += healthChange;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxhealth);

        healthbar.SetHealth(currentHealth);
    
    
    
    }

}
