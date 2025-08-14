using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Button audioToggleButton;
    private TextMeshProUGUI buttonText;
    void Start()
    {
        audioToggleButton.onClick.AddListener(toggleBGM);
        buttonText = audioToggleButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void toggleBGM()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            buttonText.text = "OFF ♪";
        }
        else
        {
            audioSource.Play();
            audioToggleButton.GetComponentInChildren<TextMeshProUGUI>().text = "ON ♪";
        }
    }
}
