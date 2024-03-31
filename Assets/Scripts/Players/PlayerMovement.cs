using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")]
    [SerializeField] private float airJumpTime;
    private float airJumpCounter;

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY; 

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if (onWall())
        {
            body.gravityScale = 2;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 2;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (isGrounded())
            {
                airJumpCounter = airJumpTime;
                jumpCounter = extraJumps;
            }
            else
                airJumpCounter -= Time.deltaTime; 
        }
    }

    private void Jump()
    {
        if (airJumpCounter <= 0 && !onWall() && jumpCounter <= 0) return;

        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
            WallJump();
        else
        {
            if (isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                if (airJumpCounter > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                else
                {
                    if (jumpCounter > 0) 
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            airJumpCounter = 0;
        }
    }

    private IEnumerator SmoothWallJump()
    {
        float duration = 0.1f;
        float time = 0;

        Vector2 initialVelocity = body.velocity;
        Vector2 targetVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY);

        while (time < duration)
        {

            body.velocity = Vector2.Lerp(initialVelocity, targetVelocity, time / duration);
            time += Time.deltaTime;
            yield return null;


            body.velocity = targetVelocity;
        }
    }

    private void WallJump()
    {
        StartCoroutine(SmoothWallJump()); 
        wallJumpCooldown = 0;
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}