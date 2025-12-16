using UnityEngine;
using UnityEngine.UI;

public class PlaytimeKeyBind : MonoBehaviour
{
    public Canvas pauseMenu;
    public Canvas skillTreeCanvas;
    public Canvas hudCanvas;

    bool gameIsPaused = false;
    float refreshTime = 1f;
    float timer = 0;


    private void Start()
    {
        gameIsPaused = false;
        pauseMenu.enabled = false;
        skillTreeCanvas.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (Input.GetKeyDown(KeyCode.Escape) && timer >= refreshTime)
        {
            TogglePause();
                timer = 0f;
            
        }
        if (Input.GetKeyDown(KeyCode.B) && timer >= refreshTime)
        {
            if (skillTreeCanvas.enabled == false)
            {
                SkillTreeClicked();
            }
            else
            {
                SkillTreeExit();
            }

        }

    }
     public void TogglePause()
    { 
        gameIsPaused = !gameIsPaused;
        pauseMenu.enabled = gameIsPaused;

        Time.timeScale = gameIsPaused ? 0f : 1f;
    
    }

    public bool IsPaused()
    { 
        return gameIsPaused;
    }

    public void SkillTreeClicked()
    {
        skillTreeCanvas.enabled = true;
        TogglePause();
        hudCanvas.enabled = false;
        pauseMenu.enabled = false;

    }

    public void SkillTreeExit()
    {
        skillTreeCanvas.enabled = false;
        TogglePause();
        hudCanvas.enabled = true;
        pauseMenu.enabled = false;

    }

}
