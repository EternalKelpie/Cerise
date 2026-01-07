using UnityEngine;
using System.Collections;

public class ShieldAbility : MonoBehaviour
{
    public GameObject shield;

    [SerializeField]float shieldDuration = 3f;
    [SerializeField] float cooldown = 3f;

    bool isOnCooldown = false;

    public bool IsShieldActive { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shield.SetActive(false);
    }
    public void SetShield(bool active)
    {
        IsShieldActive = active;
    }

    // Update is called once per frame
    public void ActivateShield()
    {
        if (!isOnCooldown)
        {
            StartCoroutine(ActivateShieldCor());
        }
    }

    IEnumerator ActivateShieldCor()
    {
        isOnCooldown = true;
        shield.SetActive(true);
        IsShieldActive = true;

        yield return new WaitForSeconds(shieldDuration);

        shield.SetActive(false);

        yield return new WaitForSeconds(shieldDuration);
        isOnCooldown = false;
        IsShieldActive = false;

    }
}
