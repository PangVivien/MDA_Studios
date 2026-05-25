using UnityEngine;

public class LeftFlipper : MonoBehaviour
{
    public Transform anchorPoint;
    public Transform flipperVisual;
    public float rotationAngle = 45f;
    public float rotationSpeed = 300f;
    public KeyCode inputKey = KeyCode.Space;

    private Quaternion restRotation;
    private Quaternion pressedRotation;
    private bool isMoving = false;

    void Start()
    {
        restRotation = flipperVisual.localRotation;
        pressedRotation = Quaternion.Euler(0, 0, rotationAngle);
    }

    void Update()
    {
        if (Input.GetKey(inputKey))
        {
            flipperVisual.localRotation = Quaternion.RotateTowards(
                flipperVisual.localRotation,
                pressedRotation,
                rotationSpeed * Time.deltaTime
            );
            isMoving = true;
        }
        else if (isMoving)
        {
            flipperVisual.localRotation = Quaternion.RotateTowards(
                flipperVisual.localRotation,
                restRotation,
                rotationSpeed * Time.deltaTime
            );

            if (flipperVisual.localRotation == restRotation)
                isMoving = false;
        }
    }
}
