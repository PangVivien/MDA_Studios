using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PreviewPostCard : MonoBehaviour
{
    [SerializeField] private RectTransform captureArea;
    [SerializeField] private GameObject targetPage;
    [SerializeField] private RawImage targetImage;

    [SerializeField] private RawImage printImage;

    private Texture2D currentTexture;
    private string savePath;

    [SerializeField] private bool rotatePreview = true;

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "preview.jpg");
    }
    public void DoneButton()
    {
        StartCoroutine(CaptureAndPreview());
    }

    IEnumerator CaptureAndPreview()
    {
        yield return StartCoroutine(CaptureCoroutine());
        ShowPreview();
    }

    public void ResetAll()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Deleted saved preview: " + savePath);
        }

        if (targetImage != null)
        {
            if (targetImage.texture != null && targetImage.texture != currentTexture)
            {
                Destroy(targetImage.texture);
            }
            targetImage.texture = null;
            targetImage.uvRect = new Rect(0, 0, 1, 1); 
        }

        if (printImage != null)
        {
            if (printImage.texture != null && printImage.texture != currentTexture)
            {
                Destroy(printImage.texture);
            }
            printImage.texture = null;
            printImage.uvRect = new Rect(0, 0, 1, 1); 
        }

        if (currentTexture != null)
        {
            Destroy(currentTexture);
            currentTexture = null;
        }
    }

    public void CapturePreview()
    {
        StartCoroutine(CaptureCoroutine());
    }

    public void ShowPreview()
    {
        if (targetPage != null)
            targetPage.SetActive(true);

        if (File.Exists(savePath))
        {
            byte[] bytes = File.ReadAllBytes(savePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);

            Texture2D finalTexture = texture;
            if (rotatePreview)
            {
                finalTexture = RotateTexture(texture);
                Destroy(texture);
            }

            targetImage.texture = finalTexture;
            if (printImage != null)
                printImage.texture = finalTexture;

            StartCoroutine(TextureAspectRatio(finalTexture));

            if (printImage != null)
                StartCoroutine(PrintAspectRatio(finalTexture));
        }
    }

    IEnumerator PrintAspectRatio(Texture2D texture)
    {
        yield return null;

        if (printImage != null && texture != null)
        {
            RectTransform rt = printImage.GetComponent<RectTransform>();

            float textureAspect = (float)texture.width / (float)texture.height;
            float imageAspect = rt.rect.width / (float)rt.rect.height;

            Rect uvRect = new Rect(0, 0, 1, 1);

            if (textureAspect > imageAspect)
            {
                float scale = imageAspect / textureAspect;
                uvRect.width = scale;
                uvRect.x = (1f - scale) / 2f;
            }
            else
            {
                float scale = textureAspect / imageAspect;
                uvRect.height = scale;
                uvRect.y = (1f - scale) / 2f;
            }

            printImage.uvRect = uvRect;
        }
    }

    IEnumerator TextureAspectRatio(Texture2D texture)
    {
        yield return null;

        if (targetImage != null && texture != null)
        {
            RectTransform rt = targetImage.GetComponent<RectTransform>();

            float textureAspect = (float)texture.width / (float)texture.height;
            float imageAspect = rt.rect.width / (float)rt.rect.height;

            Rect uvRect = new Rect(0, 0, 1, 1);

            if (textureAspect > imageAspect)
            {
                float scale = imageAspect / textureAspect;
                uvRect.width = scale;
                uvRect.x = (1f - scale) / 2f;
            }
            else
            {
                float scale = textureAspect / imageAspect;
                uvRect.height = scale;
                uvRect.y = (1f - scale) / 2f;
            }

            targetImage.uvRect = uvRect;
        }
    }

    private Texture2D RotateTexture(Texture2D original)
    {
        int originalWidth = original.width;
        int originalHeight = original.height;

        Texture2D rotatedTexture = new Texture2D(originalHeight, originalWidth, original.format, false);

        Color[] originalPixels = original.GetPixels();
        Color[] rotatedPixels = new Color[originalPixels.Length];

        for (int y = 0; y < originalHeight; y++)
        {
            for (int x = 0; x < originalWidth; x++)
            {
                int newX = originalHeight - 1 - y;
                int newY = x;
                rotatedPixels[newY * originalHeight + newX] = originalPixels[y * originalWidth + x];
            }
        }

        rotatedTexture.SetPixels(rotatedPixels);
        rotatedTexture.Apply();

        return rotatedTexture;
    }

    IEnumerator CaptureCoroutine()
    {
        yield return new WaitForEndOfFrame();

        Vector3[] corners = new Vector3[4];
        captureArea.GetWorldCorners(corners);

        Vector2[] screenCorners = new Vector2[4];
        for (int i = 0; i < 4; i++)
        {
            screenCorners[i] = RectTransformUtility.WorldToScreenPoint(null, corners[i]);
        }

        float minX = screenCorners[0].x;
        float maxX = screenCorners[0].x;
        float minY = screenCorners[0].y;
        float maxY = screenCorners[0].y;

        for (int i = 1; i < 4; i++)
        {
            minX = Mathf.Min(minX, screenCorners[i].x);
            maxX = Mathf.Max(maxX, screenCorners[i].x);
            minY = Mathf.Min(minY, screenCorners[i].y);
            maxY = Mathf.Max(maxY, screenCorners[i].y);
        }

        minX = Mathf.Clamp(minX, 0, Screen.width);
        maxX = Mathf.Clamp(maxX, 0, Screen.width);
        minY = Mathf.Clamp(minY, 0, Screen.height);
        maxY = Mathf.Clamp(maxY, 0, Screen.height);

        int width = Mathf.RoundToInt(maxX - minX);
        int height = Mathf.RoundToInt(maxY - minY);

        if (width <= 0 || height <= 0)
        {
            Debug.LogError($"Invalid capture size after clamping: {width}x{height}");
            yield break;
        }

        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);

        Rect readRect = new Rect(minX, minY, width, height);

        try
        {
            texture.ReadPixels(readRect, 0, 0);
            texture.Apply();

            Texture2D rotatedTexture = RotateTexture(texture);

            byte[] bytes = rotatedTexture.EncodeToJPG(80);
            File.WriteAllBytes(savePath, bytes);

            currentTexture = rotatedTexture;

            Destroy(texture);

            Debug.Log($"Preview Saved and rotated: {width}x{height} -> {rotatedTexture.width}x{rotatedTexture.height} at {savePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to capture: {e.Message}");
        }
    }

    public void CopyToPrint()
    {
        if (printImage != null && targetImage != null && targetImage.texture != null)
        {
            Texture2D sourceTexture = (Texture2D)targetImage.texture;
            Texture2D finalTexture = sourceTexture;

            if (rotatePreview)
            {
                finalTexture = RotateTexture(sourceTexture);
            }

            printImage.texture = finalTexture;
            StartCoroutine(PrintAspectRatio(finalTexture));
        }
    }

    public void RotateCurrentPreview()
    {
        if (targetImage != null && targetImage.texture != null)
        {
            Texture2D currentTex = (Texture2D)targetImage.texture;
            Texture2D rotatedTex = RotateTexture(currentTex);

            targetImage.texture = rotatedTex;
            if (printImage != null)
                printImage.texture = rotatedTex;

            StartCoroutine(TextureAspectRatio(rotatedTex));
            if (printImage != null)
                StartCoroutine(PrintAspectRatio(rotatedTex));
        }
    }
}
