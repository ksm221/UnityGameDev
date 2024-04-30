using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField] public GameObject creditsUI;
    [SerializeField] public GameObject defaultUI;
    [SerializeField] public GameObject creditsSoundManager;  // Assuming this is the GameObject that should be active for credits

    private void Start()
    {
        creditsUI.SetActive(false);  // Ensure the credits UI is hidden initially
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Make sure your player GameObject has the tag "Player"
        {
            defaultUI.SetActive(false);

            // Disable the default SoundManager instance
            if (SoundManager.instance != null)
            {
                SoundManager.instance.gameObject.SetActive(false);
            }

            creditsUI.SetActive(true);  // Show the credits UI when player enters the trigger
            creditsSoundManager.SetActive(true);  // Activate the credits specific sound manager
        }
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // This line is for quitting the play mode in the editor
#endif
    }
}
