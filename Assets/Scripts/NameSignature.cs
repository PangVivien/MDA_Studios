using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameSignature : MonoBehaviour
{
    public TMP_InputField nameInput;

    void OnEnable()
    {
        if (nameInput != null)
            // nameInput.text = "";
            nameInput.text = DataStorage.Instance.initials ?? "";

        nameInput.onValueChanged.AddListener(SaveName);
    }

    void OnDisable()
    {
        nameInput.onValueChanged.RemoveListener(SaveName);
    }

    void UpdateState()
    {
        bool hasName = !string.IsNullOrWhiteSpace(nameInput.text);

    }

    public void SaveName(string value)
    {
        if(DataStorage.Instance != null)
            DataStorage.Instance.initials = value;
    }
}
