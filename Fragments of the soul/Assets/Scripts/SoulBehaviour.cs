using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SoulBehaviour : MonoBehaviour
{
    public SpriteRenderer popup;
    float canInteractDistance = 1f;
    public Player player;
    [SerializeField] string sceneToLoadName;
    private bool isSaving = false;
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
                StartCoroutine(SaveAndLoad());
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

    private IEnumerator SaveAndLoad()
    {
        isSaving = true;
        Debug.Log("Soul fragment collected!");

        
        player.AddSkillPoint();

        
        SaveManager.Instance.SaveMainStatsGame();

        
        yield return new WaitForSeconds(0.1f);

        
        LoadTestScene();
    }
}
