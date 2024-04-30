using UnityEngine;

public class CreditUIManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // This line is for quitting the play mode in the editor
#endif
    }
}
