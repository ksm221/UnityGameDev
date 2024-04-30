using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private GameObject teleportDestinationObject;
    public float delaySeconds = 2f; // Delay in seconds before teleporting
                                    //public Animator playerAnimator; // Reference to the player's Animator component

    [SerializeField] private Camera startingRoomCamera; // Assign in Unity Inspector
    [SerializeField] private Camera mainCamera; // Assign in Unity Inspector
    //[SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCamera; // Assign in Unity Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportAfterDelay(other));
        }
    }

    private IEnumerator TeleportAfterDelay(Collider2D player)
    {
        // Trigger the transition animation
        //if (playerAnimator != null)
        //{
        //    playerAnimator.SetTrigger("transition");
        //}

        // Wait for the specified delay
        yield return new WaitForSeconds(delaySeconds);

        // Teleport the player after the delay
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            playerRigidbody.position = teleportDestinationObject.transform.position;

            // After teleporting, deactivate the starting room camera and activate the main camera
            if (startingRoomCamera != null)
            {
                startingRoomCamera.gameObject.SetActive(false);
            }
            if (mainCamera != null)
            {
                mainCamera.gameObject.SetActive(true);
                //virtualCamera.gameObject.SetActive(true);
            }
        }
    }
}
