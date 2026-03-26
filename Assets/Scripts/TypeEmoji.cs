using UnityEngine;

using System.Collections;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TypeEmoji : MonoBehaviour
{
    public TMP_InputField typeMessage;
    public string emojiTag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InsertEmoji()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        if (typeMessage == null) return;

        OSKeyBoard.KeyBoard.blockKeyboard = true;

        int caret = typeMessage.stringPosition;
        string text = typeMessage.text;

        string tag = $"<sprite name=\"{emojiTag}\">";
        text = text.Insert(caret, tag);

        typeMessage.text = text;
        typeMessage.stringPosition = caret + tag.Length;
        typeMessage.caretPosition = caret + tag.Length;

        typeMessage.Select();
        typeMessage.ActivateInputField();
        typeMessage.ForceLabelUpdate();

        OSKeyBoard.KeyBoard.StartCoroutine(UnblockKeyboardNextFrame());
    }
    private IEnumerator UnblockKeyboardNextFrame()
    {
        yield return null;
        OSKeyBoard.KeyBoard.blockKeyboard = false;
    }
}
