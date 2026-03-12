using UnityEngine;

public class DownloadPage : MonoBehaviour
{
    [SerializeField] public GameObject downloadPanel;
    [SerializeField] public GameObject buttonsPanel;

    public void OpenDownload()
    {
        downloadPanel.SetActive(true);
        buttonsPanel.SetActive(false);
    }

    public void CloseDownload()
    {
        downloadPanel.SetActive(false);
        buttonsPanel.SetActive(true);
    }
}
