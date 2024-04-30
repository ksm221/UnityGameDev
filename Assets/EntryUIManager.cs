using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryUIManager : MonoBehaviour
{

    [Header("Entry Screen")]
    [SerializeField] private GameObject entryScreen;
    [SerializeField] private GameObject controlsScreen;

    private void Awake()
    {
        entryScreen.SetActive(true);
    }

    private void Update()
    {
    }

    #region Start Game
    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    #endregion

    public void Quit()
    {
        Application.Quit();
    }

    public void SondVolume(bool status)
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume(bool status)
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    public void DisplayControls()
    {
        entryScreen.SetActive(false);
        controlsScreen.SetActive(true);
    }

    public void ReturnToMenu()
    {
        entryScreen.SetActive(true);
        controlsScreen.SetActive(false);
    }
}
