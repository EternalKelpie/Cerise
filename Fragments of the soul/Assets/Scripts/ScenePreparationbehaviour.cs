using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class ScenePreparationbehaviour : MonoBehaviour
{

    //Todo: add loading in skills and stuff

    public Image imageForFadeIn;
    public float fadeDuration = 1f;
    [SerializeField] bool LoadStats; // if false, do not load
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/mainStats.json") && LoadStats)
        {
            SaveManager.Instance.LoadMainStats();
        }
        else {

            Debug.LogWarning("NO SAVE FILE to load");
        }
            StartCoroutine(FadeInOut());

        SaveManager.Instance.SaveGame(3); // third save slot
    }


    public void StartFade()
    {
            
        StartCoroutine(FadeInOut());
            
    }

    public IEnumerator FadeInOut()
    {

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration / 2)
        {

            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / (fadeDuration / 2));
        imageForFadeIn.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration / 2)
        {

            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / (fadeDuration / 2));
        imageForFadeIn.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    imageForFadeIn.color = new Color(1, 1, 1, 0);
    }

}
