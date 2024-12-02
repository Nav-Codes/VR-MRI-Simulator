using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComfortController : MonoBehaviour
{
    [Header("Light Controls")]
    [SerializeField] private Light[] lights; // Array of lights to control
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TextMeshProUGUI brightnessText;

    [Header("Fan Controls")]
    [SerializeField] private Slider fanSpeedSlider;
    [SerializeField] private TextMeshProUGUI fanSpeedText;
    [SerializeField] private AudioSource fanAudioSource;
    [SerializeField] private float maxFanVolume = 0.5f;

    private void Start()
    {
        // Initialize brightness slider
        if (brightnessSlider != null)
        {
            brightnessSlider.value = 0;
            brightnessSlider.wholeNumbers = true;
            brightnessSlider.onValueChanged.AddListener(UpdateLightsBrightness);
            UpdateBrightnessText(brightnessSlider.value);
        }

        // Initialize fan speed slider
        if (fanSpeedSlider != null)
        {
            fanSpeedSlider.value = 0;
            fanSpeedSlider.wholeNumbers = true;
            fanSpeedSlider.onValueChanged.AddListener(UpdateFanSpeed);
            UpdateFanSpeedText(fanSpeedSlider.value);
        }

        // Ensure fan sound is stopped initially
        if (fanAudioSource != null)
        {
            fanAudioSource.loop = true;
            fanAudioSource.Stop();
        }
    }

    // Update lights brightness
    private void UpdateLightsBrightness(float value)
    {
        foreach (Light light in lights)
        {
            if (light != null)
            {
                light.intensity = (float)(value * 0.01);
            }
        }
        UpdateBrightnessText(value);
    }

    // Update brightness text
    private void UpdateBrightnessText(float value)
    {
        if (brightnessText != null)
        {
            brightnessText.text = value == 0 ? "Off" : $"{Mathf.RoundToInt(value)}%";
        }
    }

    // Update fan speed and sound
    private void UpdateFanSpeed(float value)
    {
        if (fanSpeedSlider != null)
        {
            if (value > 0)
            {
                if (!fanAudioSource.isPlaying)
                {
                    fanAudioSource.Play();
                }

                fanAudioSource.pitch = Mathf.Lerp(0.8f, 1.5f, value / fanSpeedSlider.maxValue);
                fanAudioSource.volume = Mathf.Lerp(0.1f, maxFanVolume, value / fanSpeedSlider.maxValue);
            }
            else
            {
                fanAudioSource.Stop();
            }
            UpdateFanSpeedText(value);
        }
    }

    // Update fan speed text
    private void UpdateFanSpeedText(float value)
    {
        if (fanSpeedText != null)
        {
            fanSpeedText.text = value == 0 ? "Off" : $"{Mathf.RoundToInt(value)}";
        }
    }
}
