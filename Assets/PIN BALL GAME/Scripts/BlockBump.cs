using UnityEngine;

public class BlockBump : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            SFXManager.Instance?.BlockSFX();

            // Optional: Add Force
            Rigidbody2D ball = collision.rigidbody;
            Vector2 direction = (ball.position - (Vector2)transform.position).normalized;
            ball.AddForce(direction * 10f, ForceMode2D.Impulse);

            Debug.Log("Block Hit!");
        }
    }
}
