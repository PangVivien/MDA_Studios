
using UnityEngine;

public class OptionPage : MonoBehaviour
{
    public GameObject option01;
    public GameObject option02;
    public GameObject option03;
    public GameObject option04;

    GameObject currentOption;

    [SerializeField] public GameObject subText;
    [SerializeField] public GameObject selectButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowOption(option01);
        subText.SetActive(true);
        selectButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowOption(GameObject selected)
    {
        option01.SetActive(false);
        option02.SetActive(false);
        option03.SetActive(false);
        option04.SetActive(false);

        selected.SetActive(true);
        subText.SetActive(false);
        selectButton.SetActive(true);

    }

    public void Option01() => ShowOption(option01);
    public void Option02() => ShowOption(option02);
    public void Option03() => ShowOption(option03);
    public void Option04() => ShowOption(option04);
}
