using UnityEngine;
using UnityEngine.UI;

public class DataStorage : MonoBehaviour
{
    public static DataStorage Instance;

    public string message = "";
    public Texture2D signature;
    public Texture2D photo;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetData()
    {
        message = "";
        photo = null;
    }

    public void SaveSignature(Texture2D tex)
    {
        signature = new Texture2D(tex.width, tex.height, tex.format, false);
        signature.SetPixels(tex.GetPixels());
        signature.Apply();
    }
}
