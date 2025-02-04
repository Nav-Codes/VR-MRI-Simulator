using UnityEngine;
using UnityEngine.EventSystems;

public class CrosshairButton : MonoBehaviour, IPointerClickHandler
{
    public SpotlightController spotlightController;

    public void OnPointerClick(PointerEventData eventData)
    {
        spotlightController.ToggleSpotlight();
    }
}