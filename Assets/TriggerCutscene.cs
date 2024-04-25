using UnityEngine;

public class TriggerCutscene : MonoBehaviour
{
    public GameObject wallLeftGameObject; // Assign the WallLeft GameObject in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable the CutSceneAndBarrierTrigger GameObject, which this script is attached to
            gameObject.SetActive(false);

            // Enable the WallLeft GameObject
            if (wallLeftGameObject != null)
            {
                wallLeftGameObject.SetActive(true);
            }

            // Placeholder for playing cutscene
            PlayCutscene();
        }
    }

    private void PlayCutscene()
    {
        // Placeholder for cutscene logic
        Debug.Log("Cutscene would play here.");

        // After cutscene logic, you may want to do additional things such as:
        // - Re-enable the trigger GameObject if needed
        // - Move the player to a specific location
        // - Trigger other game events
    }
}
