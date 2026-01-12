using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string scene = "IntroScene";
    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }


}
