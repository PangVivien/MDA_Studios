using UnityEngine;
using UnityEngine.UI;

public class LayOutSelect : MonoBehaviour
{
    public static LayOutSelect instance;

    public GameObject[] layouts;
    // public Button[] buttons;
    private int selectedLayout = -1;

    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        SelectLayOut(0);
    }

    void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLayOut(int index)
    {
        selectedLayout = index;
        Debug.Log("Option_0" + index);

        ButtonStates();
    }

    public void ApplyLayOut()
    {
        for (int i = 0; i < layouts.Length; i++)
        {
            layouts[i].SetActive(i == selectedLayout);
        }
    }

    private void ButtonStates()
    {
        
    }
}
