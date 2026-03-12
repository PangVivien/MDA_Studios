using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TypeMessage : MonoBehaviour
{
    public TMP_InputField messageInput;
    public PageManager pageManager;

    public GameObject nextButton;

    void Start()
    {
        messageInput.onSubmit.RemoveAllListeners();
        messageInput.lineType = TMP_InputField.LineType.MultiLineNewline;
    }

    void OnEnable()
    {
        if(messageInput != null)
            messageInput.text = "";

        if(nextButton != null)
            nextButton.SetActive(false);

        messageInput.onValueChanged.AddListener(CheckInput);
    }

    void OnDisable()
    {
        messageInput.onValueChanged.RemoveListener(CheckInput);
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
            return;
        }

        string[] words = text.Trim().Split(new char[] {' '}, System.StringSplitOptions.RemoveEmptyEntries);
        nextButton.SetActive(words.Length >= 1);
    }

    public void SaveMessage()
    {
        DataStorage.Instance.message = messageInput.text;
    }
}
