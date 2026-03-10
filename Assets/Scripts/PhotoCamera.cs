using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;
using System;

public class PhotoCamera : MonoBehaviour
{
    public RawImage rawImage;
    public Material greyScale;
    private WebCamTexture webcamTexture;

    private Texture2D capturePhoto;

    private bool isCaptured = false;

    [SerializeField] private GameObject completeButton;

    private void OnEnable()
    {
        StartCamera();
        completeButton.SetActive(false);
    }

    public void StartCamera()
    {
        if(rawImage == null)
            rawImage = GetComponent<RawImage>();

        if (WebCamTexture.devices.Length == 0)
        {
            Debug.Log("No WebCam Found");
            return;
        }

        WebCamDevice device = WebCamTexture.devices[0];
        webcamTexture = new WebCamTexture(device.name);

        rawImage.texture = webcamTexture;
            
        if(greyScale != null)
        {
            rawImage.material = greyScale;
            rawImage.material.mainTexture = webcamTexture;
        }

        // rawImage.uvRect = new Rect(1, 0, -1, 1);
        webcamTexture.Play();
        isCaptured = false;

        Invoke(nameof(SetCameraView), 0.2f);

         //AspectRatioFitter fitter = rawImage.GetComponent<AspectRatioFitter>();
         //if(fitter != null)
         //    fitter.aspectRatio = (float)webcamTexture.width / webcamTexture.height;

    }

    public void StopCamera()
    {
        if(webcamTexture != null && webcamTexture.isPlaying)
        {
            CapturePhoto();
            webcamTexture.Stop();

            // webcamTexture = null;
            // rawImage.texture = null;
        }
    }

    public void CapturePhoto()
    {
        if(webcamTexture == null || !webcamTexture.isPlaying) 
            return;
       
        capturePhoto = new Texture2D(webcamTexture.width, webcamTexture.height);
        capturePhoto.SetPixels(webcamTexture.GetPixels());
        capturePhoto.Apply();

        webcamTexture.Stop();
        rawImage.texture = capturePhoto;
        isCaptured = true;

        completeButton.SetActive(true);
    }

    public void RetakePhoto()
    {
        if(webcamTexture == null)
        {
            StartCamera();
            return;
        }

        rawImage.texture = webcamTexture;
        webcamTexture.Play();  
        isCaptured = false;

        Invoke(nameof(SetCameraView), 0.2f);

        completeButton.SetActive(false);
    }

    void SetCameraView()
    {
        if (webcamTexture == null) return;

        FitCameraToFrame();
        // MirrorCamera();
    }

    void MirrorCamera()
    {
        rawImage.uvRect = new Rect(1, 0, -1, 1);
    }

    void FitCameraToFrame()
    {
        float rawRatio = rawImage.rectTransform.rect.width / rawImage.rectTransform.rect.height;
        float camRatio = (float)webcamTexture.width / webcamTexture.height;

        Rect rect = new Rect(0, 0, 1, 1);

        if (camRatio > rawRatio)
        {
            float scale = rawRatio / camRatio;
            rect.width = scale;
            rect.x = (1f - scale) / 2f;
        }
        else
        {
            float scale = rawRatio / camRatio;
            rect.height = scale;
            rect.y = (1f - scale) / 2f;
        }

        rawImage.uvRect = rect;
    }
}
