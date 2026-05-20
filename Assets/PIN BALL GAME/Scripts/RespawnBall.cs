using UnityEngine;

public class RespawnBall : MonoBehaviour
{
    public GameObject ball;
    public Transform respawnPoint;
    public string SpawnTag = "Respawn";
    public float respawnDelay = 1f;      
    public float stayInHoleTime = 1.5f;  

    private Rigidbody2D ballRb;
    private bool isRespawning = false;

    void Start()
    {
        if (ball != null)
        {
            ballRb = ball.GetComponent<Rigidbody2D>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == ball && !isRespawning)
        {
            StartCoroutine(RespawnBallCoroutine());
        }
    }

    System.Collections.IEnumerator RespawnBallCoroutine()
    {
        isRespawning = true;

        if (ball != null)
        {
            if (ballRb != null)
            {
                ballRb.linearVelocity = Vector2.zero;
                ballRb.angularVelocity = 0f;
                ballRb.isKinematic = true;  
            }

            // Optional: Change Ball Color
            SpriteRenderer ballSprite = ball.GetComponent<SpriteRenderer>();
            if (ballSprite != null)
            {
                ballSprite.color = Color.gray;  
            }

            Debug.Log("Ball in hole! Waiting " + stayInHoleTime + " seconds...");

            // Wait at Hole
            yield return new WaitForSeconds(stayInHoleTime);

            ball.SetActive(false);
            Debug.Log("Ball Disabled");

            yield return new WaitForSeconds(respawnDelay);

            ball.transform.position = respawnPoint.position;

            if (ballRb != null)
            {
                ballRb.linearVelocity = Vector2.zero;
                ballRb.angularVelocity = 0f;
                ballRb.isKinematic = true;
            }

            if (ballSprite != null)
            {
                ballSprite.color = Color.white;
            }

            ball.SetActive(true);
            Debug.Log("Ball Enabled at respawn point");

            yield return new WaitForSeconds(0.1f);

            if (ballRb != null)
            {
                ballRb.isKinematic = false;
            }
        }

        isRespawning = false;
        Debug.Log("Respawn Complete");
    }
}
