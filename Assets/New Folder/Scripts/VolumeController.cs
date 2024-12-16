using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider; // Reference to the slider

    void Start()
    {
        // Set the slider value to match the current volume
        volumeSlider.value = AudioListener.volume;

        // Add a listener to call the OnVolumeChange method when the slider value changes
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    void OnVolumeChange(float value)
    {
        // Update the global audio volume
        AudioListener.volume = value;
    }

    void OnDestroy()
    {
        // Remove the listener to avoid memory leaks
        volumeSlider.onValueChanged.RemoveListener(OnVolumeChange);
    }
}
