using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public Rigidbody2D ball;
    public RectTransform springVisual;
    public Collider2D launchTrigger;

    public float launchPower = 0f;
    public float maxPower = 2000f;
    public float chargeSpeed = 1000f;
    public float compressAmount = 50f;  

    private Vector2 originalPosition;

    private bool charging = false;
    private bool canLaunch = false;
    //private bool isKinematic = false;

    void Start()
    {
        if (springVisual != null)
        {
            originalPosition = springVisual.anchoredPosition;
        }

        if (ball != null)
        {
            //ball.isKinematic = true;
        }

        if (launchTrigger == null)
        {
            launchTrigger = GetComponentInChildren<Collider2D>();
            if (launchTrigger != null)
            {
                Debug.Log("Found Trigger: " + launchTrigger.gameObject.name);
            }
        }
    }

    void Update()
    {
        // Hold BOTH Arrows
        if (canLaunch && Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.Backspace))
        {
            if (!charging)
            {
                SFXManager.Instance?.StartCharging();
            }

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

        if (charging && (!Input.GetKey(KeyCode.Space) || !Input.GetKey(KeyCode.Backspace)))
        {
            Launch();
        }
    }

    void Launch()
    {
        charging = false;
        canLaunch = false;

        SFXManager.Instance?.StopCharging();
        SFXManager.Instance?.LaunchSFX();

        if (ball != null)
        {
            //ball.isKinematic = false;
            ball.AddForce(Vector2.up * launchPower, ForceMode2D.Impulse);
        }

        if (springVisual != null)
        {
            springVisual.anchoredPosition = originalPosition;
        }

        launchPower = 0f;
        Debug.Log("Launched!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == ball.gameObject)
        {
            canLaunch = true;
            //ball.isKinematic = true;
            ball.linearVelocity = Vector2.zero;
            Debug.Log("Ball in TRIGGER");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == ball.gameObject)
        {
            canLaunch = false;
        }
    }
}
