using UnityEngine.UI;
using UnityEngine;

public class PrintPhoto : MonoBehaviour
{
    public RawImage photoImage;
    public AspectRatioFitter ratioFitter;

    void OnEnable()
    {
        if (DataStorage.Instance == null) return;
        if (DataStorage.Instance.photo == this) return;

        Texture2D photo = DataStorage.Instance.photo;

        photoImage.texture = photo;

        if (ratioFitter != null)
        {
            ratioFitter.aspectRatio = photo.width / photo.height;
        }
    }
}
