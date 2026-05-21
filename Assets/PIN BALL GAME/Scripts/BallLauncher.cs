using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public Rigidbody2D ball;
    public RectTransform springVisual; 

    public float launchPower = 0f;
    public float maxPower = 2000f;
    public float chargeSpeed = 1000f;
    public float compressAmount = 50f;  

    private Vector2 originalPosition;

    private bool charging = false;
    private bool isKinematic = false;

    void Start()
    {
        if (springVisual != null)
        {
            originalPosition = springVisual.anchoredPosition;
        }

        if (ball != null)
        {
            ball.isKinematic = true;
        }
    }

    void Update()
    {
        // Hold BOTH Arrows
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            charging = true;

            // Increase Power
            launchPower += chargeSpeed * Time.deltaTime;
            launchPower = Mathf.Clamp(launchPower, 0f, maxPower);

            if (springVisual != null)
            {
                float percent = launchPower / maxPower;
                float newY = originalPosition.y - (compressAmount * percent);
                springVisual.anchoredPosition = new Vector2(originalPosition.x, newY);
            }

            Debug.Log("Power: " + launchPower);
        }

        if (charging && (!Input.GetKey(KeyCode.LeftArrow) || !Input.GetKey(KeyCode.RightArrow)))
        {
            Launch();
        }
    }

    void Launch()
    {
        charging = false;

        if (ball != null)
        {
            ball.isKinematic = false;
            ball.AddForce(Vector2.up * launchPower, ForceMode2D.Impulse);
        }

        if (springVisual != null)
        {
            springVisual.anchoredPosition = originalPosition;
        }

        launchPower = 0f;
        Debug.Log("Launched!");
    }
}
