using UnityEngine;

public class CamTrig : MonoBehaviour
{
    public GameObject mainCameraGameObject; // Assign the Main Camera GameObject
    public GameObject bossCameraGameObject; // Assign the Boss Camera GameObject

    private float exitCooldown = 1.0f; // Cooldown in seconds before allowing an exit switch
    private float lastEnterTime;
    private bool enteredBossRoom = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !enteredBossRoom)
        {
            Debug.Log("Player entered the boss room");
            mainCameraGameObject.SetActive(false); // Disable the Main Camera GameObject
            bossCameraGameObject.SetActive(true);  // Enable the Boss Camera GameObject
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
            mainCameraGameObject.SetActive(true);  // Enable the Main Camera GameObject
            bossCameraGameObject.SetActive(false); // Disable the Boss Camera GameObject
            enteredBossRoom = false;
            other.transform.position += new Vector3(-1.5f, 0f, 0f);
        }
    }
}
