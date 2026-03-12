using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class DownloadQR : MonoBehaviour
{
    public QRGenerator qrGenerator;
    public RectTransform printArea;
    public Texture2D outcomeImage;

    public string cloudName = "dt2bk63bh";
    public string uploadImage = "MDA_Studios";
    public string imageURL;

    public GameObject[] hideObjects;
    public GameObject[] showObjects;

    public void Download()
    {
        foreach (var obj in showObjects)
            obj.SetActive(true);
        foreach (var obj in hideObjects)
            obj.SetActive(false);
       
        StartCoroutine(CaptureCoroutine());
    }

    IEnumerator CaptureCoroutine()
    {
        yield return new WaitForEndOfFrame();

        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        outcomeImage = tex;

        byte[] png = tex.EncodeToPNG();

        StartCoroutine(UploadImage(png));

        Debug.Log("Image Uploaded");

        foreach (var obj in hideObjects)
            obj.SetActive(true);
        foreach (var obj in showObjects)
            obj.SetActive(false);
    }

    IEnumerator UploadImage(byte[] pngData)
    {
        string url = "https://api.cloudinary.com/v1_1/" + cloudName + "/image/upload";

        WWWForm form = new WWWForm();
        form.AddField("upload_preset", uploadImage);
        form.AddBinaryData("file", pngData, "image.png", "image/png");

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            CloudinaryResponse res = JsonUtility.FromJson<CloudinaryResponse>(request.downloadHandler.text);
            imageURL = res.secure_url;
            Debug.Log("Image URL: " + imageURL);

            if (qrGenerator != null)
                qrGenerator.GenerateQRCodeFromURL(imageURL);
        }
        else
        {
            Debug.LogError("Upload Failed: " + request.error + "\n" + request.downloadHandler.text);
        }
    }
}

[System.Serializable]
public class CloudinaryResponse
{
    public string secure_url;
}
