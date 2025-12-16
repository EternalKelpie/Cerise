using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public Player player;
    public Canvas DeathCanvas;
    PlaytimeKeyBind pauseManager;

    private void Start()
    {
        DeathCanvas.enabled = false;
    }
    void Update()
    {
        if (player.currentHealth <= 0)
        {
            pauseManager.TogglePause();
            DeathCanvas.enabled = true;
        }
        else if (DeathCanvas.enabled == false)
        {
            DeathCanvas.enabled = false;
        }
    }
}
