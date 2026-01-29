using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneFixer : MonoBehaviour
{
    string scene = "FirstScene";
     public float nextSceneTime = 15f;
    float timer = 0;

    void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (timer >= nextSceneTime || Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(scene);
        }
    }


    
}

