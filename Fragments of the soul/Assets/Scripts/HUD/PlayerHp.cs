using UnityEngine;
using UnityEngine.UI;


public class PlayerHp : MonoBehaviour
{

    [SerializeField] float width, height;
    [SerializeField] RectTransform healthBar;

    float maxHealth, currentHealth;

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth; 
    }

    public void SetHealth(float health)
    { 
        this.currentHealth = health;
        float newWidth = (currentHealth / maxHealth) * width;

        healthBar.sizeDelta = new Vector2 (newWidth, height);
    
    
    }
}
