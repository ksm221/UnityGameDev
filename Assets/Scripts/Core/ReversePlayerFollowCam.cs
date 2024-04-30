using UnityEngine;

public class ReversePlayerFollowCam : MonoBehaviour
{
    public GameObject targetCameraObject; // Assign the Main Camera GameObject
    public GameObject mainCameraObject; // Assign the Boss Camera GameObject
    public GameObject virtualCamera; // Assign the Boss Camera GameObject

    private float exitCooldown = 0.5f; // Cooldown in seconds before allowing an exit switch
    private float lastEnterTime;
    private bool enteredBossRoom = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !enteredBossRoom)
        {
            Debug.Log("Player entered the boss room");
            targetCameraObject.SetActive(true); // Disable the Main Camera GameObject
            mainCameraObject.SetActive(false);  // Enable the Boss Camera GameObject
            virtualCamera.SetActive(false);  // Enable the Boss Camera GameObject
            enteredBossRoom = true;
            lastEnterTime = Time.time; // Record the time of entry
            other.transform.position += new Vector3(1.5f, 0f, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && enteredBossRoom && Time.time > lastEnterTime + exitCooldown)
        {
            Debug.Log("Player left the boss room");
            targetCameraObject.SetActive(false);  // Enable the Main Camera GameObject
            mainCameraObject.SetActive(true); // Disable the Boss Camera GameObject
            virtualCamera.SetActive(true);
            enteredBossRoom = false;
            other.transform.position += new Vector3(-1.5f, 0f, 0f);
        }
    }
}
