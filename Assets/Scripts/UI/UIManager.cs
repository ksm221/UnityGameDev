using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gamoverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Remove Intro Text")]
    [SerializeField] public GameObject MovementIntro;
    [SerializeField] public GameObject ExitIntroText;
    [SerializeField] public GameObject AdvancedMovementText;
    [SerializeField] public GameObject SwitchText;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
                MovementIntro.SetActive(true);
                ExitIntroText.SetActive(true);
                AdvancedMovementText.SetActive(true);
                SwitchText.SetActive(true);
            }
            else
            {
                PauseGame(true);
                MovementIntro.SetActive(false);
                ExitIntroText.SetActive(false);
                AdvancedMovementText.SetActive(false);
                SwitchText.SetActive(false);
            }
        }
    }

    #region Resume Game
    public void ResumeGame()
    {
        PauseGame(false);
        MovementIntro.SetActive(true);
        ExitIntroText.SetActive(true);
        AdvancedMovementText.SetActive(true);
        SwitchText.SetActive(true);
    }
    #endregion

    #region Game Over
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gamoverSound);
        MovementIntro.SetActive(false);
        ExitIntroText.SetActive(false);
        AdvancedMovementText.SetActive(false);
        SwitchText.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);
        Time.timeScale = status ? 0 : 1;
        if (!status)
        {
            MovementIntro.SetActive(true); // This ensures that MovementIntro is enabled when unpausing
            ExitIntroText.SetActive(true); // This ensures that MovementIntro is enabled when unpausing
            AdvancedMovementText.SetActive(true); // This ensures that MovementIntro is enabled when unpausing
            SwitchText.SetActive(true); // This ensures that MovementIntro is enabled when unpausing
        }
        else
        {
            MovementIntro.SetActive(false); // This ensures that MovementIntro is disabled when pausing
            ExitIntroText.SetActive(false); // This ensures that MovementIntro is disabled when pausing
            AdvancedMovementText.SetActive(false); // This ensures that MovementIntro is disabled when pausing
            SwitchText.SetActive(false); // This ensures that MovementIntro is disabled when pausing
        }
    }

    public void SondVolume(bool status)
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume(bool status)
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion
}
