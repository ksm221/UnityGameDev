using UnityEngine;
using System.Collections;

public class DeathBarrier : MonoBehaviour
{
    private bool hasBeenTriggered = false; // Flag to check if the barrier has been triggered
    [SerializeField] private float reactivationDelay = 1f; // Delay in seconds before reactivating

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that entered the trigger is tagged as "Player" and the barrier has not been triggered yet
        if (collision.CompareTag("Player") && !hasBeenTriggered)
        {
            hasBeenTriggered = true; // Set the flag to true to prevent future triggers

            // Get the Health component from the player GameObject
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            // Get the PlayerRespawn component from the player GameObject
            PlayerRespawn playerRespawn = collision.gameObject.GetComponent<PlayerRespawn>();

            // Ensure both Health and PlayerRespawn components are present
            if (playerHealth != null && playerRespawn != null)
            {
                // Trigger the Die method to handle the player's death
                playerHealth.Die();
            }
            else
            {
                Debug.LogError("DeathBarrier: The collided 'Player' object does not have both Health and PlayerRespawn components attached.");
            }

            // Start the coroutine to reactivate the game object after a delay
            StartCoroutine(ReactivateAfterDelay());
        }
    }

    private IEnumerator ReactivateAfterDelay()
    {
        yield return new WaitForSeconds(reactivationDelay); // Wait for the specified delay

        // Reset the hasBeenTriggered flag and reactivate the game object
        hasBeenTriggered = false;
        gameObject.SetActive(true);
    }
}
