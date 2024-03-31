using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private Transform roomTransform;
    [SerializeField] private float roomCameraSize = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraController cameraController = FindObjectOfType<CameraController>();
            if (cameraController != null)
            {
                cameraController.FocusOnRoom(roomTransform, roomCameraSize);
            }
        }
    }
}
