using UnityEngine;
using UnityEngine.EventSystems;
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
