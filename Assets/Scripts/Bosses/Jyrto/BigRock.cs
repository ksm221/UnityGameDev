using UnityEngine;

public class BigRock : EnemyDamage
{
    [SerializeField] private float resetTime;
    [SerializeField] private GameObject smallerRockPrefab; // Reference to the small rock prefab

    private float lifetime;
    private float speed;
    private Animator anim;
    private BoxCollider2D coll;
    private bool hit;
    private float direction = 1f; // Default direction to the right

    private void Awake()
    {
        Debug.Log("Big Rock Activated");
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void SetDirection(float newDirection)
    {
        direction = newDirection;
    }

    public void ActivateBigRock()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
        speed = Random.Range(2.5f, 6f);
        Debug.Log($"Big Rock Activated with speed: {speed}");
    }

    private void Update()
    {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        {
            Debug.Log("Big Rock Deactivated by Lifetime");
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectiles"))
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Big Rock Hit: {collision.name}");
            hit = true;
            base.OnTriggerEnter2D(collision);
            coll.enabled = false;
            gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Ground"))
        {
            Debug.Log($"Big Rock Hit Ground: {collision.name}");
            hit = true;
            coll.enabled = false;
            BreakIntoSmallerRocks();
            gameObject.SetActive(false);
        }
    }

    private void BreakIntoSmallerRocks()
    {
        for (int i = 0; i < 6; i++)
        {
            Vector3 position = transform.position + new Vector3(i * 1f - 0.5f, 1f, 0); // Ensure z-position is correct
            GameObject smallRockInstance = Instantiate(smallerRockPrefab, position, Quaternion.identity);
            smallRockInstance.layer = LayerMask.NameToLayer("SmallerRock");
            smallRockInstance.SetActive(true);  // Explicitly set to active
            Debug.Log($"Small Rock instantiated and activated at {position}, active status: {smallRockInstance.activeSelf}", smallRockInstance);
        }
    }



    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
