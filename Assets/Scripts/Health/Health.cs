using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private PlayerRespawn playerRespawn;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    [SerializeField] private float delay;
    private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable || dead) return;

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        Debug.Log($"Current Health: {currentHealth}");

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else if (!dead)
        {
            Die();
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    private void Die()
    {
        Debug.Log("Player Died");
        dead = true;
        SoundManager.instance.PlaySound(deathSound);
        anim.SetBool("grounded", true);
        anim.SetTrigger("die");

        // Disable components to prevent actions
        foreach (Behaviour component in components)
        {
            component.enabled = false;
        }

        StartCoroutine(PostDeathActions());
    }

    private IEnumerator PostDeathActions()
    {
        yield return new WaitForSeconds(1);

        if (gameObject.tag == "Player" && playerRespawn != null)
        {
            playerRespawn.CheckRespawn();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetHealth()
    {
        currentHealth = startingHealth;
        dead = false;
        gameObject.SetActive(true);

        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }

        anim.SetBool("grounded", true);
        anim.Play("Idle");
        StartCoroutine(Invulnerability());
    }
}
