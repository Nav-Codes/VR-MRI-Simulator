using UnityEngine;
using UnityEngine.EventSystems;
public class BasicLightButton : MonoBehaviour
{
    public ButtonLightController buttonLightController;
    public GameObject lightObject;
    public string buttonName;
    protected bool isOn = false;

    public void Start()
    {
        isOn = lightObject.activeSelf;
    }

    public bool getState()
    {
        return isOn;
    }

    public void TurnOn() {
        buttonLightController.TurnButtonOn(buttonName);
        isOn = true;
    }

    public void TurnOff() {
        buttonLightController.TurnButtonOff(buttonName);
        isOn = false;
    }
}
