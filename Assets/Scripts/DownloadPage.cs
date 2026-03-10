using UnityEngine;

public class DownloadPage : MonoBehaviour
{
    [SerializeField] public GameObject dowmloadPanel;
    [SerializeField] public GameObject LogoMDA;

    public void OpenDownload()
    {
        dowmloadPanel.SetActive(true);
        LogoMDA.SetActive(true);
    }

    public void CloseDownload()
    {
        dowmloadPanel.SetActive(false);
        LogoMDA.SetActive(false);   
    }
}
