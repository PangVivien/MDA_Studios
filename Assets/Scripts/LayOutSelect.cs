using UnityEngine;
using UnityEngine.UI;

public class LayOutSelect : MonoBehaviour
{
    public static LayOutSelect layoutSelect;

    public GameObject[] layouts;

    private int selectedLayout = -1;

    void Awake()
    {
        layoutSelect = this;
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
