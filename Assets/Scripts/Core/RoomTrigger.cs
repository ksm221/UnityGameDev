using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private Transform roomTransform;
    [SerializeField] private float roomCameraSize = 5f;
    [SerializeField] private float cooldown = 2f;

    private bool playerInside = false;
    private float lastActivationTime = -Mathf.Infinity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !playerInside)
        {
            playerInside = true;
            Debug.Log("Enter Trigger Activated");
            ActivateCameraControl();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerInside)
        {
            playerInside = false;
            Debug.Log("Exit Trigger Activated");
            DeactivateCameraControl();
            lastActivationTime = Time.time;  // Reset cooldown on exit
        }
    }


    private void ActivateCameraControl()
    {
        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController != null)
        {
            cameraController.FocusOnRoom(roomTransform, roomCameraSize);
        }
    }

    private void DeactivateCameraControl()
    {
        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController != null)
        {
            cameraController.StartFollowingPlayer();
        }
    }
}
