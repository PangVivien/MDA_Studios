using UnityEngine;
using UnityEngine.UI;

public class WhiteSignature : MonoBehaviour
{
    public RawImage previewImage;
    public Material invertMaterial;

    public void ShowWhiteSignature()
    {
        previewImage.texture = DataStorage.Instance.signature;
        previewImage.material = invertMaterial;
    }

    public void ShowNormalSignature()
    {
        previewImage.material = null;
    }
}
