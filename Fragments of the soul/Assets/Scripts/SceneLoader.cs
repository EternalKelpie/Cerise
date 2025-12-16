using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string scene = "SampleScene";
    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }


}
