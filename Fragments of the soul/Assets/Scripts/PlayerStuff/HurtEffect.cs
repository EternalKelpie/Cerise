using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class HurtEffect : MonoBehaviour
{
    public Image imageForFadeIn;
    public float fadeDuration = 0.5f;

    public void StartHurtScreen()
    {

        StartCoroutine(HurtScreen());

    }

    public IEnumerator HurtScreen()
    {

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration / 2)
        {

            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / (fadeDuration / 2));
            imageForFadeIn.color = new Color(0.5f , 0, 0, alpha);
            yield return null;
        }
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration / 2)
        {

            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / (fadeDuration / 2));
            imageForFadeIn.color = new Color(0.5f , 0, 0, alpha);
            yield return null;
        }
        imageForFadeIn.color = new Color(1, 1, 1, 0);
    }
}
