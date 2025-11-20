using UnityEngine;

public class Player : MonoBehaviour
{

    public float currentHealth, maxhealth;
   
    public PlayerHp healthbar;
    public GameObject foxFire;
    private GameObject clone;

    float projectileSpeed = 10f;
    float attackValue = 10;
    float defense = 5; // min 0 max 99

    float skillUpgradePoint;

    void Start()
    {
        healthbar.SetMaxHealth(maxhealth);
        skillUpgradePoint = 2;                      // todo: change it, it should be added gradually
    }


    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            GetDamage(-20f);
        }
        if (Input.GetKeyDown("l"))
        {
            GetDamage(20f);
        }


    }

    public void GetDamage(float healthChange)
    { 
        currentHealth -= healthChange - (0.01f * defense * healthChange);
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
    public void setAttackValue( float newValue)
    {
         attackValue = newValue;
    }
    public void ScaleAttack(float newValue)
    {
        attackValue *= newValue;
    }

    public float getDefValue()
    {
        return defense;
    }
    public void ScaleDef(float newValue)
    {
        defense *= newValue;
    }
    public void ScaleHp(float newValue)
    {
        maxhealth *= newValue;
    }

    public float GetSkillUpgradePoint()
    {
        return skillUpgradePoint;
    }
    public void SpendSkillUpgradePoint()
    {
        --skillUpgradePoint;
    }

}
