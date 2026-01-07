using UnityEngine;
using UnityEngine.SceneManagement;

public class SoulBehaviour : MonoBehaviour
{
    public SpriteRenderer popup;
    float canInteractDistance = 1f;
    public Player player;
    [SerializeField] string sceneToLoadName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        popup.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= canInteractDistance)
        {
            popup.enabled = true;
            if (Input.GetKey(KeyCode.E) )
            {
                Debug.Log(" soul fragment collected! ");
                SaveManager.Instance.SaveMainStatsGame();
                player.AddSkillPoint();
                LoadTestScene();
            }
        }
        else 
        {
            popup.enabled = false;

        }
    }

    public void LoadTestScene()
    {
        SceneManager.LoadScene(sceneToLoadName);
    }

}
