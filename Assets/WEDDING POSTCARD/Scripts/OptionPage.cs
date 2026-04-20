
using UnityEngine;
using UnityEngine.UI;

public class OptionPage : MonoBehaviour
{
    // public MovablePhoto movablePhoto;

    public GameObject option01;
    public GameObject option02;
    public GameObject option03;
    public GameObject option04;

    public GameObject optionPage;
    // public GameObject previewPage;
    public GameObject selectButton;
    public GameObject editButton;


    private void OnEnable()
    {
        if (optionPage != null && optionPage.activeInHierarchy)
        {
            ResetDefault();
        }

    }

    public void ResetDefault()
    {
        ShowOption(option01);

        selectButton.SetActive(false);
        editButton.SetActive(false);
        
        // previewPage.SetActive(true);
}

    public void ShowOption(GameObject selected)
    {
        option01.SetActive(false);
        option02.SetActive(false);
        option03.SetActive(false);
        option04.SetActive(false);

        selected.SetActive(true);

        selectButton.SetActive(true);
        editButton.SetActive(true);
    }

    public void Option01() => ShowOption(option01);
    public void Option02() => ShowOption(option02);
    public void Option03() => ShowOption(option03);
    public void Option04() => ShowOption(option04);
}
