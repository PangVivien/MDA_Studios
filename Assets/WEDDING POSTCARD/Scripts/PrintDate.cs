using UnityEngine;
using System;
using TMPro;

public class PrintDate : MonoBehaviour
{
    public TMP_Text dateText;
    // public TMP_Text previewText;
    private int lastDay = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateDate();
    }

    // Update is called once per frame
    void Update()
    {
        if(DateTime.Now.Day != lastDay)
        {
            UpdateDate();
        }
    }

    void UpdateDate()
    {
        DateTime now = DateTime.Now;
        // previewText.text = now.Day + DaySuffix(now.Day) + " . " + now.ToString("MMMM") + " . " + now.ToString("yyyy");
        dateText.text = now.Day + DaySuffix(now.Day) + " . " + now.ToString("MMMM") + " . " + now.ToString("yyyy");
        lastDay = now.Day;
    }

    string DaySuffix(int day)
    {
        if (day % 10 == 1 && day != 11) return "st";
        if (day % 10 == 2 && day != 12) return "nd";
        if (day % 10 == 3 && day != 13) return "rd";
        return "th";
    }
}
