using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is either a Wall, an Enemy, or a Rock
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Rocks"))
        {
            Debug.Log("Hit Object: " + collision.gameObject.name + ", Layer: " + LayerMask.LayerToName(collision.gameObject.layer));
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("explode");
            gameObject.SetActive(false);  // Ensure the projectile is deactivated in all cases

            // Handle additional logic for enemies
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Health enemyHealth = collision.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(1);
                }
            }
        }
    }


    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}