using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// This class represents a basic light button that interacts with a <see cref="ButtonLightController"/> to control the state of a light.
/// It provides methods to turn the light on and off, and it tracks the current state of the light (on/off).
/// The light's state is stored and can be retrieved using the <see cref="getState"/> method.
/// </summary>
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
