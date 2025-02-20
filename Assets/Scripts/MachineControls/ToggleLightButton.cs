using UnityEngine;
using UnityEngine.EventSystems;
public class ToggleLightButton : MonoBehaviour, IPointerClickHandler
{
    public ButtonLightController buttonLightController;
    public GameObject lightObject;
    public string buttonName;
    private bool isOn = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isOn)
        {
            buttonLightController.TurnButtonOff(buttonName);
            isOn = false;
        }
        else
        {
            buttonLightController.TurnButtonOn(buttonName);
            isOn = true;
        }
    }
}
