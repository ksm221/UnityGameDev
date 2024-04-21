using UnityEngine;

public class Rock : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;
    private float direction = 1f; // Default direction to the right

    private void Awake()
    {
        Debug.Log("Rock Activated");
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void SetDirection(float newDirection)
    {
        direction = newDirection;
    }

    public void ActivateRock()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        {
            Debug.Log("Rock Deactivated by Lifetime");
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectiles"))
        {
            return;
        }
        if (collision.CompareTag("Trap"))
        {
            return;
        }
        if (collision.CompareTag("Triggers"))
        {
            return;
        }

        Debug.Log($"Rock Hit: {collision.name}");
        hit = true;
        base.OnTriggerEnter2D(collision);
        coll.enabled = false;
        gameObject.SetActive(false);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}