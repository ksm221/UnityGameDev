using UnityEngine;

public class SmallerRock : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    [SerializeField]public float damage; // Set this value to whatever damage a small rock should do
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        var collider = GetComponent<Collider2D>();
        collider.isTrigger = false; // Set collider to not be a trigger
        Debug.Log("Small Rock Created with non-trigger collider", gameObject);
    }

    private void Start()
    {
        // Assign a random direction along the Y axis, and a random X axis direction.
        Vector2 moveDirection = new Vector2(Random.Range(-5f, 4f), Random.Range(5f, 50f)).normalized;

        // Apply an initial force to the rock
        rb.AddForce(moveDirection * moveSpeed, ForceMode2D.Impulse);
        Debug.Log($"Small Rock initial force applied in direction {moveDirection}", gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Small Rock collided with {collision.gameObject.name}", gameObject);

        if (collision.gameObject.CompareTag("Player"))
        {
            // Damage the player
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            // Potentially destroy the small rock on collision with the player
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
