using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public PhotoCamera photoCamera;

    public GameObject homePage;
    public GameObject photoPage;
    public GameObject messagePage;
    public GameObject signaturePage;
    public GameObject layoutPage;
    public GameObject printPage;

    private GameObject currentPage;

    public Image fadeImage;
    public float fadeDuration = 0.5f;
    private bool isFading = false;

    private void Start()
    {
        ShowPage(homePage);
    }

    public void GoToPhoto() => StartCoroutine(SwitchPage(photoPage));
    public void GoToMessage() => StartCoroutine(SwitchPage(messagePage));
    public void GoToSign() => StartCoroutine(SwitchPage(signaturePage));
    public void GoToLayout() => StartCoroutine(SwitchPage(layoutPage));
    public void GoToPrint() => StartCoroutine(SwitchPage(printPage));
    public void GoToHome()
    {
        if(DataStorage.Instance != null)
            DataStorage.Instance.ResetData();

        StartCoroutine(SwitchPage(homePage));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShowPage(GameObject target)
    {
        if (currentPage != null)
            currentPage.SetActive(false);

        target.SetActive(true);
        currentPage = target;
    }

    IEnumerator SwitchPage(GameObject target)
    {
        if (isFading || currentPage == target)
            yield break;

        isFading = true;

        if (currentPage == photoPage && photoCamera != null)
            photoCamera.StopCamera();

        // FADE IN
        yield return StartCoroutine(Fade(0f, 1f));

        ShowPage(target);

        if (target == photoPage && photoCamera != null)
            photoCamera.StartCamera();

        // FADE OUT
        yield return StartCoroutine(Fade(1f, 0f));

        isFading = false;
    }

    IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        Color c = fadeImage.color;

        while(t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = to;
        fadeImage.color = c;
    }
}
