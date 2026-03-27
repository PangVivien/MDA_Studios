using UnityEngine;

public class ViewImage : MonoBehaviour
{
    [SerializeField] public GameObject printables;

    public void OpenImage()
    {
        printables.SetActive(true);
    }

    public void CloseImage()
    {
        printables.SetActive(false);
    }
}
