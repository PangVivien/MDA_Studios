using UnityEngine;

public class RespawnBall : MonoBehaviour
{
    public GameObject ball;
    public Transform respawnPoint;

    public float stayInHoleTime = 1.5f;

    private Rigidbody2D ballRb;
    private bool waitingForRespawn = false;

    void Start()
    {
        ballRb = ball.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!waitingForRespawn) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Respawn();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == ball && !waitingForRespawn)
        {
            StartCoroutine(BallLost());
        }
    }

    System.Collections.IEnumerator BallLost()
    {
        waitingForRespawn = true;

        ballRb.linearVelocity = Vector2.zero;
        ballRb.angularVelocity = 0f;
        ballRb.isKinematic = true;

        ball.SetActive(false);

        Debug.Log("Ball Lost");

        yield return new WaitForSeconds(stayInHoleTime);

        Debug.Log("Press to Respawn");
    }

    void Respawn()
    {
        ball.transform.position = respawnPoint.position;

        ball.SetActive(true);

        ballRb.isKinematic = false;
        ballRb.linearVelocity = Vector2.zero;
        ballRb.angularVelocity = 0f;

        waitingForRespawn = false;

        Debug.Log("Respawned");

        // RESET SCORE ONCE/LIFE
        ScoreManager.Instance?.ResetScore();
    }
}