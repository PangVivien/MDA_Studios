using UnityEngine;

public class FlipperController : MonoBehaviour
{
    public Rigidbody2D flipperRb;   
    public Transform flipperTip;    

    public float hitForce = 12f;
    public float upwardBoost = 3f;

    private Vector3 lastPos;
    private Vector2 velocity;

    void Start()
    {
        lastPos = transform.position;
    }

    void FixedUpdate()
    {
        velocity = (Vector2)(transform.position - lastPos) / Time.fixedDeltaTime;
        lastPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.rigidbody) return;

        if (!collision.rigidbody.CompareTag("Ball")) return;

        Rigidbody2D ball = collision.rigidbody;

        Vector2 dir = (ball.position - (Vector2)flipperTip.position).normalized;

        if (velocity.magnitude < 0.5f) return;

        Vector2 force = dir * hitForce + Vector2.up * upwardBoost;

        ball.AddForce(force, ForceMode2D.Impulse);
    }
}
