using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// This class represents a button in the UI that toggles the spotlight on or off when clicked.
/// It implements the <see cref="IPointerClickHandler"/> interface to respond to click events.
/// When the button is clicked, it calls the <see cref="SpotlightController.ToggleSpotlight"/> method to toggle the spotlight's state.
/// </summary>
public class CrosshairButton : MonoBehaviour, IPointerClickHandler
{
    public SpotlightController spotlightController;

    public void OnPointerClick(PointerEventData eventData)
    {
        spotlightController.ToggleSpotlight();
    }
}