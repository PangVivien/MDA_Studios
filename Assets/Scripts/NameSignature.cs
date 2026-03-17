using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameSignature : MonoBehaviour
{
    public TMP_InputField nameInput;

    void OnEnable()
    {
        if(nameInput != null)
            nameInput.text = "";

        UpdateState();
    }

    void UpdateState()
    {
        bool hasName = !string.IsNullOrWhiteSpace(nameInput.text);

    }

    public void SaveName()
    {
        if(DataStorage.Instance != null)
            DataStorage.Instance.initials = nameInput.text;
    }
}
