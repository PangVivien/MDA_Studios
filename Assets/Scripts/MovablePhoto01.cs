using UnityEngine;
using UnityEngine.UI;

public class MovablePhoto01 : MonoBehaviour
{
    public RawImage rawImage;

    [Header("Move Settings")]
    [SerializeField] float moveStep = 0.02f;
    [SerializeField] float minX = -0.5f;
    [SerializeField] float maxX = 0.5f;

    [Header("Zoom Settings")]
    [SerializeField] float zoomStep = 0.05f;
    [SerializeField] float minZoomWidth = 0.5f;
    [SerializeField] float maxZoomWidth = 1f;
    [SerializeField] float minZoomHeight = 0.5f;
    [SerializeField] float maxZoomHeight = 1f;

    private Rect originalUVRect;

    void Start()
    {
        if (rawImage != null)
        {
            originalUVRect = rawImage.uvRect;
        }
    }

    // MOVE UP
    public void MoveUp()
    {
        if (!gameObject.activeInHierarchy) return;

        Rect uv = rawImage.uvRect;

        uv.x -= moveStep;
        uv.x = Mathf.Clamp(uv.x, minX, maxX);

        rawImage.uvRect = uv;
    }

    // MOVE DOWN
    public void MoveDown()
    {
        if (!gameObject.activeInHierarchy) return;

        Rect uv = rawImage.uvRect;

        uv.x += moveStep;
        uv.x = Mathf.Clamp(uv.x, minX, maxX);

        rawImage.uvRect = uv;
    }

    // ZOOM IN
    public void ZoomIn()
    {
        if (!gameObject.activeInHierarchy) return;

        Rect uv = rawImage.uvRect;

        float newWidth = uv.width - zoomStep;
        float newHeight = uv.height - zoomStep;

        if (newWidth >= minZoomWidth && newHeight >= minZoomHeight)
        {
            float centerX = uv.x + (uv.width / 2);
            float centerY = uv.y + (uv.height / 2);

            uv.width = newWidth;
            uv.height = newHeight;

            uv.x = centerX - (uv.width / 2f);
            uv.y = centerY - (uv.height / 2f);

            uv.x = Mathf.Clamp(uv.x, minX, maxX);

            rawImage.uvRect = uv;
        }
    }

    // ZOOM OUT
    public void ZoomOut()
    {
        if (!gameObject.activeInHierarchy)
            return;

        Rect uv = rawImage.uvRect;

        float newWidth = uv.width + zoomStep;
        float newHeight = uv.height + zoomStep;

        if (newWidth <= maxZoomWidth && newHeight <= maxZoomHeight)
        {
            float centerX = uv.x + (uv.width / 2f);
            float centerY = uv.y + (uv.height / 2f);

            uv.width = newWidth;
            uv.height = newHeight;

            uv.x = centerX - (uv.width / 2f);
            uv.y = centerY - (uv.height / 2f);

            uv.x = Mathf.Clamp(uv.x, minX, maxX);

            rawImage.uvRect = uv;
        }
    }

    // RESET POSITION
    public void ResetPosition()
    {
        if (!gameObject.activeInHierarchy) return;

        rawImage.uvRect = originalUVRect;
    }
}
