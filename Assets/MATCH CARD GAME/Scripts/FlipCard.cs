using UnityEngine;
using UnityEngine.UI;
using System;

public class FlipCard : MonoBehaviour
{
    public GameObject backSide;
    public GameObject frontSide;
    public float flipDuration = 0.2f;
    public int cardID;

    private bool isFrontVisible = false;
    private bool isAnimating = false;

    public Action<FlipCard> CardClicked;

    void Start()
    {
        // GetComponent<Button>().onClick.AddListener(OnClick);

        backSide.SetActive(true);
        frontSide.SetActive(false);
    }
    void OnClick()
    {
        if (isAnimating || isFrontVisible) return;

        CardClicked?.Invoke(this);
    }

    public void ShowFront()
    {
        if (isAnimating || isFrontVisible) return;
        StartCoroutine(Flip(true));
    }
    public void ShowBack()
    {
        if (isAnimating || !isFrontVisible) return;
        StartCoroutine(Flip(false));
    }

    System.Collections.IEnumerator Flip(bool showFront)
    {
        isAnimating = true;

        float elapsed = 0f;
        Vector3 originalScale = transform.localScale;

        while (elapsed < flipDuration / 2f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (flipDuration / 2f);
            float scaleX = Mathf.Lerp(1f, 0f, t);
            transform.localScale = new Vector3(scaleX, originalScale.y, originalScale.z);
            yield return null;
        }

        isFrontVisible = showFront;
        backSide.SetActive(!showFront);
        frontSide.SetActive(showFront);

        elapsed = 0f;
        while (elapsed < flipDuration / 2f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (flipDuration / 2f);
            float scaleX = Mathf.Lerp(0f, 1f, t);
            transform.localScale = new Vector3(scaleX, originalScale.y, originalScale.z);
            yield return null;
        }

        transform.localScale = originalScale;
        isAnimating = false;
    }

    public void ResetCards()
    {
        if (isAnimating) return;

        if (isFrontVisible)
        {
            isFrontVisible = false;
            backSide.SetActive(true);
            frontSide.SetActive(false);
        }
    }

}
