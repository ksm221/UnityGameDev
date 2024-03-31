using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform checkpointPosition;
    private Health playerHealth;
    [SerializeField] private AudioClip checkpoint;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint" && gameObject.tag == "Player")
        {
            checkpointPosition = collision.transform;
            SoundManager.instance.PlaySound(checkpoint);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
            Debug.Log("Checkpoint set to: " + checkpointPosition.position);
        }
    }

    public void CheckRespawn()
    {
        // Check if checkpointPosition is still null
        if (checkpointPosition == null)
        {
            uiManager.GameOver();
        }
        else
        {
            transform.position = checkpointPosition.position;
            playerHealth.ResetHealth();
        }
    }
}
