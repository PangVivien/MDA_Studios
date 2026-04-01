using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggablePhoto : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public RawImage rawImage;
    public bool canDrag = false;

    public float dragSpeed = 0.001f;

    public GameObject editButton;
    public GameObject doneButton;
    public GameObject cancelButton;
    public GameObject layoutOption;
    public GameObject instructionText;

    private Vector2 lastPointerPos;

    void Awake()
    {
        if (rawImage == null)
            rawImage = GetComponent<RawImage>();
    }

    private void OnEnable()
    {
        editButton.SetActive(true);
        doneButton.SetActive(true);
        layoutOption.SetActive(true);
        cancelButton.SetActive(false);
        instructionText.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData  eventData)
    {
        if (!canDrag) return;
        lastPointerPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag) return;

        Vector2 delta = eventData.position - lastPointerPos;
        lastPointerPos = eventData.position;

        Rect uv = rawImage.uvRect;

        uv.x -= eventData.delta.x * dragSpeed;
        uv.y -= eventData.delta.y * dragSpeed;

        uv.x = Mathf.Clamp(uv.x, -1f, 1f);
        uv.y = Mathf.Clamp(uv.y, -1f, 1f);

        rawImage.uvRect = uv;
    }

    public void EnableDrag()
    {
        doneButton.SetActive(false);
        editButton.SetActive(false);
        cancelButton.SetActive(true);
        layoutOption.SetActive(false);
        instructionText.SetActive(true);
        canDrag = true;
    }

    public void DisableDrag()
    {
        doneButton.SetActive(true);
        editButton.SetActive(true);
        cancelButton.SetActive(false);
        layoutOption.SetActive(true);
        instructionText.SetActive(false);
        canDrag = false;
    }
}
