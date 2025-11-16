using UnityEngine;
using UnityEngine.UI;

public class PlaytimeKeyBind : MonoBehaviour
{
    public Canvas PauseMenu;

    bool gameIsPaused = false;
    float refreshTime = 1f;
    float timer = 0;


    private void Start()
    {
        gameIsPaused = false;
        PauseMenu.enabled = false;
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
       
    }
    void TogglePause()
    { 
        gameIsPaused = !gameIsPaused;
        PauseMenu.enabled = gameIsPaused;

        Time.timeScale = gameIsPaused ? 0f : 1f;
    
    }

    public bool IsPaused()
    { 
        return gameIsPaused;
    }
}
