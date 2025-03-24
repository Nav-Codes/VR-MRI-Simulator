using UnityEngine;

/// <summary>
/// Controls a spotlight in a Unity scene.
/// Provides functionality to toggle the spotlight on and off.
/// </summary>
public class SpotlightController : MonoBehaviour
{
    // Reference to the spotlight component
    public Light spotlight;
    /// <summary>
    /// Toggles the spotlight on or off.
    /// If the spotlight is enabled, it turns off; if it's disabled, it turns on.
    /// </summary>
    public void ToggleSpotlight()
    {
        if (spotlight != null)
        {
            spotlight.enabled = !spotlight.enabled;
        }
    }
}
