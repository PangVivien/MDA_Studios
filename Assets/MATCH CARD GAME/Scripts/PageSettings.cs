using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageSettings : MonoBehaviour
{
    [Header("Pages")]
    public GameObject homePage;
    public GameObject instructionPage;
    public GameObject gamePage;
    public GameObject winPage;
    public GameObject losePage;

    private GameObject currentPage;

    [Header("Fade Transition")]
    public Image fadeImage;
    public float fadeDuration = 0.5f;
    private bool isFading = false;

    [Header("Game Settings")]
    public GameObject[] allCards;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI gameCountdownText;
    public GameObject StartText;
    public GameObject GameText;

    private FlipCard[] cardScripts;
    private int cardsFlippedCount = 0;
    private bool isGameTimerRunning = false;
    private float gameTimer = 60f;

    void Start()
    {
        if (allCards.Length > 0)
        {
            cardScripts = new FlipCard[allCards.Length];
            for (int i = 0; i < allCards.Length; i++)
            {
                if (allCards[i] != null)
                {
                    cardScripts[i] = allCards[i].GetComponent<FlipCard>();

                    if (cardScripts[i] != null)
                    {
                        cardScripts[i].cardOpened += CardOpened;
                    }
                }
            }
        }

        ShowPage(homePage);
    }

    void Update()
    {
        if (isGameTimerRunning)
        {
            gameTimer -= Time.deltaTime;

            if (gameCountdownText != null)
            {
                int seconds = Mathf.CeilToInt(gameTimer);
                gameCountdownText.text = seconds.ToString("D2");
            }

            if (gameTimer <= 0f)
            {
                isGameTimerRunning = false;
                GoToLose();
            }
        }
    }

    public void GoToInstruction() => StartCoroutine(SwitchPage(instructionPage));
    public void GoToGame() => StartCoroutine(SwitchPage(gamePage));
    public void GoToWin() => StartCoroutine(SwitchPage(winPage));
    public void GoToLose() => StartCoroutine(SwitchPage(losePage));
    public void GoToHome() => StartCoroutine(SwitchPage(homePage));

    private void CardOpened()
    {
        cardsFlippedCount++;

        if (cardsFlippedCount >= allCards.Length)
        {
            isGameTimerRunning = false;
            StartCoroutine(WinWithDelay());
        }
    }

    public void CardFlipped(bool isNowFront)
    {
        if (!isNowFront) return;

        cardsFlippedCount++;

        if (cardsFlippedCount >= allCards.Length)
        {
            isGameTimerRunning = false;
            StartCoroutine(WinWithDelay());
        }
    }

    public void ResetAllCards()
    {
        cardsFlippedCount = 0;
        gameTimer = 60f;
        isGameTimerRunning = false;

        foreach (var card in allCards)
        {
            if (card != null)
            {
                var flipScript = card.GetComponent<FlipCard>();
                if (flipScript != null)
                {
                    flipScript.ResetCards();
                }
            }
        }
    }

    private IEnumerator WinWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        GoToWin();
    }


    private IEnumerator StartGameWithCountdown()
    {
        if (countdownText != null)
        {
            GameText.SetActive(false);
            StartText.SetActive(true);
            countdownText.gameObject.SetActive(true);
            countdownText.text = "05";
            yield return new WaitForSeconds(1f);
            countdownText.text = "04";
            yield return new WaitForSeconds(1f);
            countdownText.text = "03";
            yield return new WaitForSeconds(1f);
            countdownText.text = "02";
            yield return new WaitForSeconds(1f);
            countdownText.text = "01";
            yield return new WaitForSeconds(1f);
            countdownText.text = "GO";
            yield return new WaitForSeconds(0.5f);
            countdownText.gameObject.SetActive(false);
            StartText.SetActive(false);
            GameText.SetActive(true);
        }

        if (gameCountdownText != null)
        {
            gameCountdownText.gameObject.SetActive(true);
            gameCountdownText.text = "60";
        }

        gameTimer = 60f;
        isGameTimerRunning = true;
        EnableCardClicks(true);
    }

    private void EnableCardClicks(bool enable)
    {
        foreach (var card in allCards)
        {
            if (card != null)
            {
                var button = card.GetComponent<Button>();
                if (button != null)
                    button.interactable = enable;
            }
        }
    }

    void ShowPage(GameObject target)
    {
        if (currentPage != null)
            currentPage.SetActive(false);

        target.SetActive(true);
        currentPage = target;

        if (target == gamePage)
        {
            ResetAllCards();
            EnableCardClicks(false);
            if (gameCountdownText != null)
                gameCountdownText.gameObject.SetActive(false);
            StartCoroutine(StartGameWithCountdown());
        }
        else if (target == winPage || target == losePage)
        {
            if (gameCountdownText != null)
                gameCountdownText.gameObject.SetActive(false);
            isGameTimerRunning = false;
        }
        else if (target == homePage)
        {
            if (gameCountdownText != null)
                gameCountdownText.gameObject.SetActive(false);
            isGameTimerRunning = false;
        }
    }

    IEnumerator SwitchPage(GameObject target)
    {
        if (isFading || currentPage == target)
            yield break;

        isFading = true;

        // FADE IN
        yield return StartCoroutine(Fade(0f, 1f));

        ShowPage(target);

        // FADE OUT
        yield return StartCoroutine(Fade(1f, 0f));

        isFading = false;
    }

    IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = to;
        fadeImage.color = c;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

}
