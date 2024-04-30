using UnityEngine;
using TMPro; // This is the namespace for TextMeshPro

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro;
    private TextMeshProUGUI txt; // Change the type to TextMeshProUGUI

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>(); // Get the TextMeshProUGUI component
    }

    private void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        if(txt != null)
        {
            float volumeValue = PlayerPrefs.GetFloat(volumeName, 0f) * 100; // Add a default value in case the key does not exist
            txt.text = textIntro + Mathf.RoundToInt(volumeValue).ToString(); // Optional: round the value to an integer
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found on " + gameObject.name);
        }
    }
}
