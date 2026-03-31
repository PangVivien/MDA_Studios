using UnityEngine;
using UnityEngine.EventSystems;

public class KeepSelection : MonoBehaviour
{
    private GameObject lastSelected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            
            if (EventSystem.current.currentSelectedGameObject != lastSelected)
            {
                lastSelected = EventSystem.current.currentSelectedGameObject;
            }
        }
        else
        {
            
            if (lastSelected != null)
            {
                EventSystem.current.SetSelectedGameObject(lastSelected);
            }
        }
    }
}
