using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // Reference to the main camera
    [SerializeField] private float speed; // Speed for room transition
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed; // Speed for following the player
    [SerializeField] private float cameraSizeSpeed = 1f; // Speed of camera size transition
    [SerializeField] private float defaultCameraSize = 5f; // Default camera size

    private bool isFollowingPlayer = true; // Initially set to follow the player
    private float currentPositionX;
    private Vector3 velocity = Vector3.zero;
    private float lookAhead;
    private float targetCameraSize; // Target size for the camera

    private void Start()
    {
        if (!mainCamera)
        {
            mainCamera = Camera.main; // Automatically find the main camera if not assigned
        }
        targetCameraSize = defaultCameraSize; // Initialize with default size
    }

    private void Update()
    {
        if (isFollowingPlayer)
        {
            lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
            transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);

            // Smoothly transition back to the default camera size
            if (mainCamera.orthographicSize != defaultCameraSize)
            {
                mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, defaultCameraSize, Time.deltaTime * cameraSizeSpeed);
            }
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPositionX, transform.position.y, transform.position.z), ref velocity, speed);

            // Smoothly transition to the target camera size when focusing on a room
            if (mainCamera.orthographicSize != targetCameraSize)
            {
                mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetCameraSize, Time.deltaTime * cameraSizeSpeed);
            }
        }
    }

    public void MoveToNewRoom(Transform _newRoom, float roomCameraSize = 10f)
    {
        currentPositionX = _newRoom.position.x;
        targetCameraSize = roomCameraSize; // Adjust the camera size based on the room
        isFollowingPlayer = false; // Switch to room camera mode
    }

    public void StartFollowingPlayer()
    {
        isFollowingPlayer = true; // Switch back to player-following mode
    }

    public void FocusOnRoom(Transform roomTransform, float roomCameraSize)
    {
        currentPositionX = roomTransform.position.x;
        targetCameraSize = roomCameraSize; // Use this size to fit the room
        isFollowingPlayer = false; // Ensure the camera focuses on the room
    }

    public void OnBossFightEnd()
    {
        StartFollowingPlayer(); // Switch back to following the player
    }
}
