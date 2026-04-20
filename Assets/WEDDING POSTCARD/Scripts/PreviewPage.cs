using UnityEngine;
using System.Collections;
public class PreviewPage : MonoBehaviour
{
    public float delay = 5f;
    public float fadeDuration = 1f;

    public CanvasGroup canvasGroup;
    private Coroutine fadeRoutine;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        canvasGroup.alpha = 0;

        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(delay);

        float time = 0;

        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;
    }

    public void ResetFade()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
