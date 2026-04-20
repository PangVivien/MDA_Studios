using UnityEngine;
using System;
using TMPro;

public class AnalogDate : MonoBehaviour
{
    public TMP_Text dateText;

    void OnEnable()
    {
        UpdateDate();
    }

    void UpdateDate()
    {
        DateTime now = DateTime.Now;

        dateText.text = now.ToString("dd  MM  yy");
    }
}
