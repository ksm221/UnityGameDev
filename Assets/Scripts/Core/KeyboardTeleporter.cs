using System.Collections;
using UnityEngine;

public class KeyboardTeleporter : MonoBehaviour
{
    public Vector2 teleportDestination;
    public float delaySeconds = 2f; // Delay in seconds before teleporting

    [SerializeField] private Camera startingRoomCamera; // Assign in Unity Inspector
    [SerializeField] private Camera mainCamera; // Assign in Unity Inspector
    //[SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCamera; // Assign in Unity Inspector
    [SerializeField] private GameObject teleportDestinationObject;

    private Rigidbody2D playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        if (playerRigidbody == null)
        {
            Debug.LogError("KeyboardTeleporter requires a Rigidbody2D component on the player object.");
        }

        if (startingRoomCamera == null)
        {
            Debug.LogError("StartingRoomCamera is not assigned in the inspector!");
        }

        if (mainCamera == null)
        {
            Debug.LogError("MainCamera is not assigned in the inspector!");
        }
        //if (virtualCamera == null)
        //{
        //    Debug.LogError("VirtualCamera is not assigned in the inspector!");
        //}
    }

    private void Update()
    {
        if (startingRoomCamera != null && startingRoomCamera.gameObject.activeInHierarchy)
        {
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(TeleportAfterDelay());
            }
        }
    }

    private IEnumerator TeleportAfterDelay()
    {
        yield return new WaitForSeconds(delaySeconds);

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
