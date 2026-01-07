using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public Player player;
    public Canvas DeathCanvas;
    public PlaytimeKeyBind pauseManager;

    private void Start()
    {
        DeathCanvas.enabled = false;
    }
    void Update()
    {
        if (player.currentHealth <= 0 && DeathCanvas.enabled == false)
        {
            pauseManager.TogglePause();
            DeathCanvas.enabled = true;
        }
        else if (player.currentHealth > 0 && DeathCanvas.enabled == true)
        {
            DeathCanvas.enabled = false;
        }
    }
}
