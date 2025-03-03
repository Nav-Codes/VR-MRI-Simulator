using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// This class inherits from the BasicLightButton class and implements the IPointerClickHandler interface.
/// It controls a toggle button that turns a light on or off when clicked. 
/// The button's state alternates between on and off each time it is clicked, using the TurnOn and TurnOff methods inherited from BasicLightButton.
/// </summary>
public class ToggleLightButton : BasicLightButton, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isOn)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }
}
