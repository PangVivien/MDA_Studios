using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bumpers : MonoBehaviour
{
    [Header("Bumper Settings")]
    public int points = 1000;
    public string bumperType = "";
    public float scaleSpeed = 10f;
    public float scaleAmount = 0.5f;

    [Header("Visual Effects")]
    public Color hitColor = Color.red;
    private Vector3 originalScale;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isHitting = false;
    private float hitTimer = 0f;

    void Start()
    {
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    void Update()
    {
        // Reset After Hit
        if (isHitting)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * scaleSpeed);

            if (Vector3.Distance(transform.localScale, originalScale) < 0.01f)
            {
                transform.localScale = originalScale;
                isHitting = false;

                // Restore Color
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = originalColor;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Add Score
            ScoreManager.Instance?.AddScore(bumperType, points);

            SFXManager.Instance?.BumperSFX();
            HitAnimation();

            // Optional: Add Force
            Rigidbody2D ball = collision.rigidbody;
            Vector2 direction = (ball.position - (Vector2)transform.position).normalized;
            ball.AddForce(direction * 15f, ForceMode2D.Impulse);

            Debug.Log("Bumper Hit! +" + points + " Points");
        }
    }

    void HitAnimation()
    {
        isHitting = true;
        transform.localScale = originalScale + Vector3.one * scaleAmount;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = hitColor;
        }
    }
}
