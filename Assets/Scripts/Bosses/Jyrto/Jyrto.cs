using UnityEngine;

public class Jyrto : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] rocks;

    [Header("Head Attack")]
    [SerializeField] private Transform firepoint_2;
    [SerializeField] private GameObject[] bigRocks;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Kick Sound")]
    [SerializeField] private AudioClip kickSound;

    //References
    private Animator anim;
    private bool isFacingRight = false;
    private int kickCount = 0;
    private int headKickCount = 0;
    private bool canHeadKick = false;
    private float originalYPosition;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                if (canHeadKick)
                {
                    if (headKickCount < 3)
                    {
                        cooldownTimer = 0;
                        anim.SetTrigger("headkick");
                        //HeadKickAttack();
                        headKickCount++;
                        Debug.Log(headKickCount);
                    }

                    if (headKickCount >= 3)
                    {
                        canHeadKick = false;
                        kickCount = 0; // Reset kick count after 3 head kicks
                    }
                }
                else
                {
                    cooldownTimer = 0;
                    anim.SetTrigger("kick");
                    //KickRock();
                    kickCount++;
                    Debug.Log(kickCount);

                    if (kickCount >= 5)
                    {
                        canHeadKick = true;
                        headKickCount = 0; // Reset head kick count
                    }
                }
            }
        }
    }

    private void HeadKickAttack()
    {
        SoundManager.instance.PlaySound(kickSound);
        cooldownTimer = 0;

        int bigRockIndex = FindBigRock();
        if (bigRockIndex != -1)
        {
            GameObject bigRock = bigRocks[bigRockIndex];
            bigRock.transform.position = firepoint_2.position;

            // The direction is set based on the isFacingRight flag
            float direction = isFacingRight ? 1f : -1f;
            bigRock.GetComponent<BigRock>().SetDirection(direction);

            bigRock.GetComponent<BigRock>().ActivateBigRock();
        }
    }
    private void KickRock()
    {
        SoundManager.instance.PlaySound(kickSound);
        cooldownTimer = 0;

        int rockIndex = FindRock();
        if (rockIndex != -1)
        {
            GameObject rock = rocks[rockIndex];
            rock.transform.position = firepoint.position;

            // The direction is set based on the isFacingRight flag
            float direction = isFacingRight ? 1f : -1f;
            rock.GetComponent<Rock>().SetDirection(direction);

            rock.GetComponent<Rock>().ActivateRock();
        }
    }


    private int FindRock()
    {
        for (int i = 0; i < rocks.Length; i++)
        {
            if (!rocks[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private int FindBigRock()
    {
        for (int i = 0; i < bigRocks.Length; i++)
        {
            if (!bigRocks[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    public void JumpUp()
    {
        originalYPosition = transform.position.y;
        transform.position += 1.5f * Vector3.up; // Moves the boss up by 1.5 unit
    }

    public void JumpDown()
    {
        transform.position = new Vector3(transform.position.x, originalYPosition, transform.position.z); // Reverts to original Y position
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}