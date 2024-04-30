using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("Spikhead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float returnSpeed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private float returnDelay = 3f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackTimeout = 1f; // Time in seconds after which the attack stops if no collision occurs.
    private float timeSinceAttackStarted;
    private float checkTimer;
    private Vector3 destination;
    private bool attacking;
    private Vector3[] directions = new Vector3[4];
    private Vector3 originalPosition;
    private float lastAttackTime;

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
            timeSinceAttackStarted += Time.deltaTime;

            // Check if the attack has timed out
            if (timeSinceAttackStarted >= attackTimeout)
            {
                Debug.Log("Attack timed out. Returning to original position.");
                timeSinceAttackStarted = 0; // Reset the attack timer
                lastAttackTime = Time.time; // Update last attack time to start return delay
                Stop();
            }
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();

            // Return to original position logic
            if (!attacking && Time.time - lastAttackTime > returnDelay)
            {
                ReturnToOriginalPosition();
            }
        }
    }

    private void ReturnToOriginalPosition()
    {
        Vector3 returnDirection = originalPosition - transform.position;
        if (returnDirection.magnitude > 0.1f)
        {
            transform.position += returnDirection.normalized * returnSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = originalPosition;
            attacking = false; // Ensure attacking is false when in original position
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirections();

        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
            Debug.DrawRay(transform.position, directions[i], Color.red);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
                lastAttackTime = Time.time; // Set last attack time when starting an attack
                timeSinceAttackStarted = 0; // Reset attack timer
                break; // Break after deciding to attack in one direction
            }
        }
    }
    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    private void Stop()
    {
        destination = Vector3.zero;
        attacking = false;
        timeSinceAttackStarted = 0; // Reset the attack timer when stopping
        lastAttackTime = Time.time; // Update last attack time to start return delay
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
