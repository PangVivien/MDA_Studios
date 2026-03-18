using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OSKeyBoard : MonoBehaviour
{
    public static OSKeyBoard KeyBoard;

    public GameObject osKeyboard;
    public GameObject hideKeyBoard;
    [SerializeField] private GameObject emojiBox;
    [SerializeField] private GameObject boxButton;
    [SerializeField] private GameObject KeyBoardEmojiBox;
    [SerializeField] private GameObject KeyBoardBoxButton;
    public Animator keyboardAnimation;

    [HideInInspector] public bool blockKeyboard = false;

    private void Awake()
    {
        KeyBoard = this;
    }

    void OnEnable()
    {
        if(osKeyboard == null) 
            osKeyboard.SetActive(false);

        if(hideKeyBoard == null)
            hideKeyBoard.SetActive(false);

        if(KeyBoardBoxButton == null) KeyBoardBoxButton.SetActive(false);
    }

    public void ShowKeyBoard(TMP_InputField input)
    {
        if(blockKeyboard) return;

        emojiBox.SetActive(false);
        boxButton.SetActive(false);

        osKeyboard.SetActive(true);
        keyboardAnimation.SetTrigger("Show");
        StartCoroutine(EnableAnimation());
    }

    public void HideKeyBoard()
    {
        KeyBoardEmojiBox.SetActive(false);
        KeyBoardBoxButton.SetActive(false);
        hideKeyBoard.SetActive(false);

        boxButton.SetActive(true);
        keyboardAnimation.SetTrigger("Hide");
        StartCoroutine(DisableAnimation());
    }

    IEnumerator EnableAnimation()
    {
        yield return new WaitForSeconds(0.8f);
        KeyBoardBoxButton.SetActive(true);
        hideKeyBoard.SetActive(true);
    }

    IEnumerator DisableAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        osKeyboard.SetActive(false);
    }
}
