using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameSignature : MonoBehaviour
{
    public TMP_InputField nameInput;

    void OnEnable()
    {
        if (nameInput != null)
        {
            if (DataStorage.Instance != null && !string.IsNullOrEmpty(DataStorage.Instance.initials))
            {
                nameInput.text = DataStorage.Instance.initials;
            }
            else
            {
                nameInput.text = "";
            }

            nameInput.onValueChanged.AddListener(SaveName);
        }
    }

    void OnDisable()
    {
        nameInput.onValueChanged.RemoveListener(SaveName);
    }

    void UpdateState()
    {
        // bool hasName = !string.IsNullOrWhiteSpace(nameInput.text);

    }

    public void SaveName(string value)
    {
        if (DataStorage.Instance != null)
        {
            DataStorage.Instance.initials = value;
            Debug.Log($"Name saved: {value}");
        }
    }
}
