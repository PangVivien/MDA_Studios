using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("TOTAL SCORE")]
    public TextMeshProUGUI scoreText;

    [Header("INDIVIDUAL SCORES")]
    public TextMeshProUGUI sendMoneyText;
    public TextMeshProUGUI billPaymentText;
    public TextMeshProUGUI remittanceText;
    public TextMeshProUGUI mobileTopupText;
    public TextMeshProUGUI cardTransactionText;

    private int totalScore = 0;

    private int sendMoneyScore = 0;
    private int billPaymentScore = 0;
    private int remittanceScore = 0;
    private int mobileTopupScore = 0;
    private int cardTransactionScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(string bumperType, int points)
    {
        totalScore += points;

        switch (bumperType)
        {
            case "SendMoney":
                sendMoneyScore += points;
                break;

            case "BillPayment":
                billPaymentScore += points;
                break;

            case "Remittance":
                remittanceScore += points;
                break;

            case "MobileTopup":
                mobileTopupScore += points;
                break;

            case "CardTransaction":
                cardTransactionScore += points;
                break;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        // TOTAL
        scoreText.text = FormatScore(totalScore);

        // INDIVIDUAL
        sendMoneyText.text = "$" + sendMoneyScore;
        billPaymentText.text = "$" + billPaymentScore;
        remittanceText.text = "$" + remittanceScore;
        mobileTopupText.text = "$" + mobileTopupScore;
        cardTransactionText.text = "$" + cardTransactionScore;
    }

    string FormatScore(int score)
    {
        return score.ToString("D6").Insert(3, ",");
    }

    public void ResetScore()
    {
        totalScore = 0;
        sendMoneyScore = 0;
        billPaymentScore = 0;
        remittanceScore = 0;
        mobileTopupScore = 0;
        cardTransactionScore = 0;

        UpdateUI();

        Debug.Log("All Scores Reset");
    }

}
