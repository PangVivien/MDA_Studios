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

        if (card.IsMatched()) return;

        if (card == firstCard) return;

        if (firstCard == null)
        {
            firstCard = card;
            card.ShowFront();
        }
        else if (secondCard == null)
        {
            secondCard = card;
            card.ShowFront();

            isChecking = true; 

            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstCard.cardID == secondCard.cardID)
        {
            firstCard.SetMatched();
            secondCard.SetMatched();

            firstCard.SetClickable(false);
            secondCard.SetClickable(false);

            SoundManager.Instance?.PlayCardPaired();

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
