using UnityEngine;
using TMPro;

public class PrintMessage : MonoBehaviour
{
    public TMP_Text messageText;

    void OnEnable()
    {
        if(messageText == null)
        {
            Debug.LogError("No Message Data.");
            return;
        }

        if(messageText != null)
            messageText.text = DataStorage.Instance.message;

    }
}
