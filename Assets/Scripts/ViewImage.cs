using UnityEngine;

public class ViewImage : MonoBehaviour
{
    [SerializeField] public GameObject printables;

    public void CloseImage()
    {
        printables.SetActive(false);
    }
}
