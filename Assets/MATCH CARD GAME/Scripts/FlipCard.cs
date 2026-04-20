using UnityEngine;
using UnityEngine.UI;
using System;

public class FlipCard : MonoBehaviour
{
    public GameObject backSide;   
    public GameObject frontSide;  
    public float flipDuration = 0.2f; 

    private bool isFrontVisible = false;
    private bool isAnimating = false;
    private Button button;

    public event Action cardOpened;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(CardClick);

        backSide.SetActive(true);
        frontSide.SetActive(false);
    }

    public void CardClick()
    {
        if (isAnimating) return;
        if (isFrontVisible) return;

        if (flipDuration > 0)
            StartCoroutine(FlipWithScale());
        else
            ToggleImmediate();
    }

    void ToggleImmediate()
    {
        isFrontVisible = !isFrontVisible;
        backSide.SetActive(!isFrontVisible);
        frontSide.SetActive(isFrontVisible);

        cardOpened?.Invoke();
    }

    System.Collections.IEnumerator FlipWithScale()
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

        isFrontVisible = true;
        backSide.SetActive(false);
        frontSide.SetActive(true);
        cardOpened?.Invoke();

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
