using UnityEngine;
using UnityEngine.UI;

public class DataStorage : MonoBehaviour
{
    public static DataStorage Instance;

    public string message = "";
    public string initials;
    public Texture2D signature;
    public Texture2D photo;

    public Texture2D photoData;
    public string messageData = "";
    public Texture2D signatureData;
    public Texture2D LayOutData;
    public string uploadedURL;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        signature = null;
        initials = "";
        messageData = null;
        photoData = null;
        signatureData = null;
        LayOutData = null;
        uploadedURL = null;
    }

    public void SaveSignature(Texture2D tex)
    {
        signature = new Texture2D(tex.width, tex.height, tex.format, false);
        Graphics.CopyTexture(tex, signature);
        signature.Apply();
    }

    public void SaveLayout(Texture2D tex)
    {
        LayOutData = new Texture2D(tex.width, tex.height, tex.format, false);
        Graphics.CopyTexture(tex, LayOutData);
        LayOutData.Apply();
    }
}
