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
            lastAttackTime = Time.time;
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();

            if (Time.time - lastAttackTime > returnDelay)
            {

                Vector3 returnDirection = originalPosition - transform.position;
                if (returnDirection.magnitude > 0.1f)
                {
                    transform.position += returnDirection.normalized * returnSpeed * Time.deltaTime;
                }
                else
                {
                    transform.position = originalPosition;
                }
            }
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirections();

        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
