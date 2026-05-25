using UnityEngine;

public class FlipperController : MonoBehaviour
{
    public Rigidbody2D flipperRb;
    public Transform flipperTip;

    public float hitForce = 5000f;
    public float upwardBoost = 1000f;

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

        Vector2 contactPoint = collision.contacts[0].point;
        Vector2 direction = (contactPoint - (Vector2)flipperTip.position).normalized;


        float flipperSpeed = Mathf.Max(velocity.magnitude, 5f); 


        Vector2 force = direction * (hitForce * flipperSpeed) + Vector2.up * upwardBoost;

        ball.AddForce(force, ForceMode2D.Impulse);

        Debug.Log("Flipper hit! Speed: " + flipperSpeed + " Force: " + force.magnitude);
    }
}
