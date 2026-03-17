using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PrintSignature : MonoBehaviour
{
    // public RawImage previewSignature;
    public RawImage savedSignature;
    public TMP_Text nameText;

    void OnEnable()
    {
        if(DataStorage.Instance == null) return;

        if(DataStorage.Instance.signature != null)
        {
            // previewSignature.texture = DataStorage.Instance.signature;
            savedSignature.texture = DataStorage.Instance.signature;
            // savedSignature.SetNativeSize();
            Debug.Log("Signature Loaded");
        }
        else
        {
            Debug.Log("No Signature");
        }

        if (DataStorage.Instance != null && nameText != null)
        {
            nameText.text = DataStorage.Instance.initials ?? "";
        }
    }

}
