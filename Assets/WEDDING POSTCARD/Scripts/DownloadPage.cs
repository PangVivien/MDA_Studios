using UnityEngine;

public class DownloadPage : MonoBehaviour
{
    public GameObject printables;

    [SerializeField] public GameObject downloadPanel;
    [SerializeField] public GameObject buttonsPanel;

    public void OpenDownload()
    {
        printables.SetActive(true);

        downloadPanel.SetActive(true);
        buttonsPanel.SetActive(false);
    }

    public void CloseDownload()
    {
        printables.SetActive(false);

        downloadPanel.SetActive(false);
        buttonsPanel.SetActive(true);
    }
}
