using UnityEngine;

public class HealthBarBehaviour : MonoBehaviour
{
[SerializeField] public Sprite healthBarSprite;
protected GameObject healthBar;
protected GameObject healthBarFill;
[SerializeField] Vector3 healthBarOffset = new Vector3(0f, 2f, 0);
[SerializeField] float scaler = 100f;


    public void CreateHealthBar(float healthPercentage)
    {
        healthBar = new GameObject("Healthbar");
        healthBar.transform.SetParent(transform);
        healthBar.transform.localScale = Vector3.one * scaler;
        healthBar.transform.localPosition = healthBarOffset;

        healthBarFill = new GameObject("Fill");
        healthBarFill.transform.SetParent(healthBar.transform);
        healthBarFill.transform.localScale = Vector3.one * scaler;
        //healthBarFill.transform.localScale = Vector3.one * scaler;    new Vector3(healthBarWidth, healthBarHeight, 1f);
        healthBarFill.transform.localPosition = Vector3.zero;
        var fillRenderer = healthBarFill.AddComponent<SpriteRenderer>();
        fillRenderer.sprite = healthBarSprite;
        fillRenderer.color = Color.green;
        fillRenderer.sortingOrder = 10;

        UpdateHealthBar(1f);
    }

    public void UpdateHealthBar(float healthPercentage)
    {
        if (healthBar == null)
        {
            return;
        }

        if (float.IsNaN(healthPercentage) || float.IsInfinity(healthPercentage))
        {
            healthPercentage = 0f; // albo 1f przy starcie
        }

        healthPercentage = Mathf.Clamp01(healthPercentage);
        //float healthPercentage = hp / maxHp;
        healthBarFill.transform.localScale = new Vector3(healthPercentage, 1f, 1);


        var healthBarRenderer = healthBarFill.GetComponent<SpriteRenderer>();
        healthBarRenderer.color = Color.Lerp(Color.red, Color.green, healthPercentage);


        if (healthPercentage <= 0)
        {
            healthBar.SetActive(false);
        }
    }
}
