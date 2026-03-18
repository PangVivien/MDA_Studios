using UnityEngine;

public class OpenEmoji : MonoBehaviour
{
    public GameObject emojiPanel;
    public GameObject keyboardEmoji;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emojiPanel.SetActive(false);
        keyboardEmoji.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterEmoji()
    {
        emojiPanel.SetActive(true);
    }

    public void EnterKeyBoard()
    {
        keyboardEmoji.SetActive(true);
    }

    public void ExitEmoji()
    {
        emojiPanel.SetActive(false);
    }

    public void ExitKeyBoard()
    {
        keyboardEmoji.SetActive(false);
    }
}
