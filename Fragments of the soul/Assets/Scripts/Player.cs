using UnityEngine;

public class Player : MonoBehaviour
{
    public float projectileSpeed = 10f;
    public float attackValue = 10;

    public PlayerHp healthbar;
    public float currentHealth, maxhealth;
    public GameObject foxFire;
    private GameObject clone;
    

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

    public void attack(Vector3 mousePosition)
    {
        Vector3 direction = (mousePosition - transform.position).normalized;

        clone = Instantiate(foxFire, gameObject.transform.localPosition, Quaternion.identity);
        clone.GetComponent<FoxFireBehaviour>().Init(this);

        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }




    public float getAttackValue()
    {
        return attackValue;
    }


}
