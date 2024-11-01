using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VRFade : MonoBehaviour
{
    public Image fadeImage; 
    public float fadeDuration = 1f; // Duration for fade in/out

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Call this function to start fading to black
    public void StartFadeToBlack()
    {
        StartCoroutine(FadeToBlack());
    }

    // Call this function to start fading out from black
    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    // Fade from transparent to black
    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    // Fade from black to transparent
    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }
    }

    // Example of a fade in from black at the start of the scene
    private IEnumerator FadeIn()
    {
        fadeImage.color = new Color(0, 0, 0, 1); // Start with black
        yield return FadeOut(); // Fade out from black
    }

}
