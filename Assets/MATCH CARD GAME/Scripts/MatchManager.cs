using UnityEngine;
using System.Collections;

public class MatchManager : MonoBehaviour
{
    public FlipCard firstCard;
    public FlipCard secondCard;

    private bool isChecking = false;
    private int matchedPairs = 0;
    public int totalPairs = 8;

    public PageSettings pageSettings;

    public void CardClicked(FlipCard card)
    {
        if (isChecking) return;

        if (firstCard == null)
        {
            firstCard = card;
            card.ShowFront();
        }
        else if (secondCard == null)
        {
            secondCard = card;
            card.ShowFront();
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        isChecking = true;

        yield return new WaitForSeconds(0.5f);

        if (firstCard.cardID == secondCard.cardID)
        {
            matchedPairs++;

            if (matchedPairs >= totalPairs)
            {
                pageSettings.GoToWin();
            }
        }
        else
        {
            firstCard.ShowBack();
            secondCard.ShowBack();
        }

        firstCard = null;
        secondCard = null;
        isChecking = false;
    }

    public void ResetGame()
    {
        matchedPairs = 0;
        firstCard = null;
        secondCard = null;
        isChecking = false;
    }
}
