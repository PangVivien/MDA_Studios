using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;

public class TypeMessage : MonoBehaviour
{
    public TMP_SpriteAsset emojiAssets;
    public TMP_InputField messageInput;
    public PageManager pageManager;

    public GameObject nextButton;

    public TMP_Text wordcountText;
    public int maxCount = 150;

    private string lastText = "";

    void Start()
    {

        messageInput.lineType = TMP_InputField.LineType.MultiLineNewline;
    }

    void OnEnable()
    {
        // messageInput.text = "";
        // nextButton.SetActive(false);
        messageInput.textComponent.spriteAsset = emojiAssets;

        if (DataStorage.Instance != null && DataStorage.Instance.message != null)
        {
            messageInput.text = DataStorage.Instance.message ?? "";
            nextButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(false);
        }

        wordcountText.text = "0/" + maxCount;
        lastText = messageInput.text;

        messageInput.onValueChanged.RemoveAllListeners();
        messageInput.onValueChanged.AddListener(OnTextChanged);
    }

    void OnDisable()
    {
        messageInput.onValueChanged.RemoveListener(OnTextChanged);
    }

    public void Next()
    {
        if(messageInput != null)
            DataStorage.Instance.message = messageInput.text;

        if (pageManager != null)
            pageManager.GoToSign();
    }

    void CheckInput(string text)
    {
        if(string.IsNullOrWhiteSpace(text))
        {
            nextButton.SetActive(false);

            UpdateCount(0);

            return;
        }

        string processed = Regex.Replace(text,"<.*?>", "E");

        int count = processed.Length;

        if (count > maxCount)
        {
            return;
        }

        // string[] words = text.Trim().Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

        nextButton.SetActive(count >= 1);

        UpdateCount(count);
    }

    void OnTextChanged(string currentText)
    {
        if (string.IsNullOrEmpty(currentText))
        {
            UpdateCount(0);
            nextButton.SetActive(false);
            lastText = currentText;
            return;
        }

        int caret = messageInput.stringPosition;

        if (currentText.Length < lastText.Length)
        {
            if (caret > 0 && currentText[caret - 1] == '>')
            {
                int start = currentText.LastIndexOf('<', caret - 1);
                if (start != -1)
                {
                    currentText = currentText.Remove(start, caret - start);
                    messageInput.text = currentText;
                    messageInput.caretPosition = start;
                    caret = start;
                }
            }
        }

        string temp = Regex.Replace(currentText, "<.*?>", "E");
        int count = temp.Length;

        if (count > maxCount)
        {
            int diff = count - maxCount;

            currentText = temp.Substring(0, maxCount);
            messageInput.text = currentText;
            messageInput.caretPosition = Mathf.Min(caret, currentText.Length);
            count = maxCount;
        }

        nextButton.SetActive(count > 0);
        UpdateCount(count);

        lastText = messageInput.text;
    }


    void UpdateCount(int count)
    {
        wordcountText.text = $"{count}/{maxCount}";
    }

    public void SaveMessage()
    {
        DataStorage.Instance.message = messageInput.text;
    }
}
