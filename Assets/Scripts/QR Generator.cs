using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class QRGenerator : MonoBehaviour
{
    public RawImage QRCodeDisplay; 
    public int QRCodeSize = 256;

    public DownloadQR download;

    [ContextMenu("Generate Random QR Code")]
    public void GenerateRandomQRCode()
    {
        string randomCode = System.Guid.NewGuid().ToString("N"); 
        Debug.Log("Generated Code: " + randomCode);

        Texture2D qrTexture = GenerateQRCodeTexture(randomCode, QRCodeSize, QRCodeSize);

        QRCodeDisplay.texture = qrTexture;
        QRCodeDisplay.rectTransform.sizeDelta = new Vector2(100, 100);

        if (download != null)
        {
            download.outcomeImage = qrTexture;
        }
    }

    public void GenerateQRCodeFromURL(string url)
    {
        Debug.Log("QR Link to: " + url);

        Texture2D qrTexture = GenerateQRCodeTexture(url, QRCodeSize, QRCodeSize);

        QRCodeDisplay.texture = qrTexture;
        QRCodeDisplay.rectTransform.sizeDelta = new Vector2(100, 100);
    }

    private Texture2D GenerateQRCodeTexture(string text, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Width = width,
                Height = height,
                Margin = 1
            }
        };

        Color32[] pixelData = writer.Write(text);

        Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
        tex.SetPixels32(pixelData);
        tex.Apply();
        return tex;
    }
}
