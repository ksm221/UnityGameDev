using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform checkpointPosition;
    private Camera checkpointCamera;  // Camera associated with the current checkpoint
    private Health playerHealth;
    [SerializeField] private AudioClip checkpoint;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();  // Ensure there's a UIManager in the scene
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint" && gameObject.tag == "Player")
        {
            checkpointPosition = collision.transform;  // Assign the Transform of the checkpoint
            checkpointCamera = collision.GetComponent<CheckpointCam>().RoomCamera;  // Get the camera from the checkpoint component
            SoundManager.instance.PlaySound(checkpoint);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
            Debug.Log("Checkpoint set to: " + checkpointPosition.position);
        }
    }

    public void CheckRespawn()
    {
        if (checkpointPosition == null)
        {
            uiManager.GameOver();
        }
        else
        {
            transform.position = checkpointPosition.position;  // Use the position of the Transform
            playerHealth.ResetHealth();
            ActivateCheckpointCamera();
        }
    }

    private void ActivateCheckpointCamera()
    {
        if (checkpointCamera != null)
        {
            // Deactivate all other room cameras
            foreach (var cam in FindObjectsOfType<Camera>())
            {
                cam.gameObject.SetActive(false);
            }
            // Activate the checkpoint's camera
            checkpointCamera.gameObject.SetActive(true);
        }
    }
}

