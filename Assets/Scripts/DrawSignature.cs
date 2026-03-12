using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;

public class DrawSignature : MonoBehaviour
{
    public RawImage signaturePreview;
    private Texture2D signatureTexture;

    public RawImage drawImage;
    public RawImage printImage;
    public Color drawColor = Color.black;
    public int brushSize = 2;

    Texture2D tex;
    Vector2 lastPos;
    bool drawing = false;

    void OnEnable()
    {
        if(tex != null)
        {
            Erase();
        }
    }

    void Start()
    {
        tex = new Texture2D(512, 512, TextureFormat.RGBA32, false);
        tex. filterMode = FilterMode.Bilinear;

        Color[] pixels = new Color[512 * 512];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = Color.clear;

        tex.SetPixels(pixels);
        tex.Apply();

        drawImage.texture = tex;
    }

    void Update()
    {
        if(Pointer.current.press.isPressed)
        {
            Vector2 pos = Pointer.current.position.ReadValue();

            Vector2 local;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                drawImage.rectTransform, pos, null, out local );

            Vector2 texPos = new Vector2(local.x + tex.width / 2, local.y + tex.height / 2);


            if(!drawing)
            {
                lastPos = texPos;
                drawing = true;
            }

            DrawLine(lastPos, texPos);
            lastPos = texPos;

            tex.Apply();
        }
        else
        {
            drawing = false;
        }
    }

    void DrawLine(Vector2 from, Vector2 to)
    {
        float dist = Vector2.Distance(from, to);

        for (float i = 0; i < dist; i++)
        {
            Vector2 pos = Vector2.Lerp(from, to, i / dist);
            Brush((int)pos.x, (int)pos.y);
        }

    }

    void Brush(int cx, int cy)
    {
        for(int x = -brushSize; x <= brushSize; x++)
        {
            for(int y = -brushSize; y <= brushSize; y++)
            {
                if(x * x + y * y <= brushSize * brushSize)
                {
                    int px = cx + x;
                    int py = cy + y;

                    if (px >= 0 && px < tex.width && py >= 0 && py < tex.height)
                        tex.SetPixel(px, py, drawColor);
                }
            }
        }
    }

    public void Erase()
    {
        Color[] pixels = new Color[tex.width * tex.height];

        for(int i = 0; i < pixels.Length; i++)
            pixels[i] = Color.clear;

        tex.SetPixels(pixels);
        tex.Apply();

        drawing = false;
    }

    public void OnSubmit()
    {

        if (tex == null) return;

        Texture2D copy = new Texture2D(tex.width, tex.height, tex.format, false);
        Graphics.CopyTexture(tex, copy);
        copy.Apply();

        DataStorage.Instance.signature = copy;

        Debug.Log("Signature Saved");

    }

    public void SignatureData()
    {
        DataStorage.Instance.SaveSignature(tex);
        signaturePreview.texture = tex;
    }
}
