using UnityEngine;

public class OpenEmoji : MonoBehaviour
{
    [SerializeField] public GameObject emojiPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emojiPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterEmoji()
    {
        emojiPanel.SetActive(true);
    }

    public void ExitEmoji()
    {
        emojiPanel.SetActive(false);
    }
}
