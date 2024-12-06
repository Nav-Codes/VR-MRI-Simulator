using UnityEngine;

public class SpotlightController : MonoBehaviour
{
    public Light spotlight;
    public void ToggleSpotlight()
    {
        if (spotlight != null)
        {
            spotlight.enabled = !spotlight.enabled;
        }
    }
}
