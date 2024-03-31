using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float cameraSizeSpeed = 1f;
    [SerializeField] private float defaultCameraSize = 5f;

    private bool isFollowingPlayer = true;
    private float currentPositionX;
    private Vector3 velocity = Vector3.zero;
    private float lookAhead;
    private float targetCameraSize;

    private void Start()
    {
        if (!mainCamera)
        {
            mainCamera = Camera.main;
        }
        targetCameraSize = defaultCameraSize;
    }

    private void Update()
    {
        if (isFollowingPlayer)
        {
            lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
            transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);

            if (mainCamera.orthographicSize != defaultCameraSize)
            {
                mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, defaultCameraSize, Time.deltaTime * cameraSizeSpeed);
            }
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPositionX, transform.position.y, transform.position.z), ref velocity, speed);

            if (mainCamera.orthographicSize != targetCameraSize)
            {
                mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetCameraSize, Time.deltaTime * cameraSizeSpeed);
            }
        }
    }

    public void MoveToNewRoom(Transform _newRoom, float roomCameraSize = 10f)
    {
        currentPositionX = _newRoom.position.x;
        targetCameraSize = roomCameraSize;
        isFollowingPlayer = false;
    }

    public void StartFollowingPlayer()
    {
        isFollowingPlayer = true;
    }

    public void FocusOnRoom(Transform roomTransform, float roomCameraSize)
    {
        currentPositionX = roomTransform.position.x;
        targetCameraSize = roomCameraSize;
        isFollowingPlayer = false;
    }

    public void OnBossFightEnd()
    {
        StartFollowingPlayer();
    }
}
