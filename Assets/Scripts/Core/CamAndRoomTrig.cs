using UnityEngine;

public class CamAndRoomTrig : MonoBehaviour
{
    public GameObject StartingRoomCam; // Assign the Main Camera GameObject
    public GameObject AdvancedMovementCam; // Assign the Boss Camera GameObject

    private float exitCooldown = 1.0f; // Cooldown in seconds before allowing an exit switch
    private float lastEnterTime;
    private bool enteredNextRoom = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !enteredNextRoom)
        {
            Debug.Log("Player entered the next room");
            StartingRoomCam.SetActive(false); // Disable the Main Camera GameObject
            AdvancedMovementCam.SetActive(true);  // Enable the Boss Camera GameObject
            enteredNextRoom = true;
            lastEnterTime = Time.time; // Record the time of entry
            other.transform.position += new Vector3(1.5f, 0f, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && enteredNextRoom && Time.time > lastEnterTime + exitCooldown)
        {
            Debug.Log("Player left room");
            StartingRoomCam.SetActive(true);  // Enable the Main Camera GameObject
            AdvancedMovementCam.SetActive(false); // Disable the Boss Camera GameObject
            enteredNextRoom = false;
            other.transform.position += new Vector3(-1.5f, 0f, 0f);
        }
    }
}
