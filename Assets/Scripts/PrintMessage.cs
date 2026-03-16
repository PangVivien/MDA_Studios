using UnityEngine;
using TMPro;

public class PrintMessage : MonoBehaviour
{
    public TMP_Text messageText;
    // public TMP_Text previewText;

    void OnEnable()
    {
        if (messageText == null)
        {
            Debug.LogError("No Message Text assigned.");
            return;
        }

        messageText.text = DataStorage.Instance.message ?? "";
        // previewText.text = DataStorage.Instance.message ?? "";

    }
}
