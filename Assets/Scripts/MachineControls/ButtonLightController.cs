using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class controls the button light indicators for various buttons, such as up, down, park, dock, power, and hand buttons.
/// It manages the state of each button light, turning them on or off based on the button's state.
/// Additionally, the class handles the activation of non-power button lights when the power button is turned on.
/// The class uses dictionaries to map button names to their corresponding light objects and their states.
/// </summary>
public class ButtonLightController : MonoBehaviour
{
    public GameObject upButtonLight;
    public GameObject downButtonLight;
    public GameObject ParkButtonLight;
    public GameObject DockButtonLight;
    public GameObject powerButtonLight;
    public GameObject rightHandButtonLight;
    public GameObject leftHandButtonLight;
    public GameObject NonPowerButtons;

    private Dictionary<string, GameObject> buttonLights;
    private Dictionary<string, bool> buttonStates;

    private void Start()
    {
        buttonLights = new Dictionary<string, GameObject>
        {
            { "Up", upButtonLight },
            { "Down", downButtonLight },
            { "Park", ParkButtonLight },
            { "Dock", DockButtonLight },
            { "Power", powerButtonLight },
            { "RightHand", rightHandButtonLight },
            { "LeftHand", leftHandButtonLight }
        };

        buttonStates = new Dictionary<string, bool>
        {
            { "Up", false },
            { "Down", false },
            { "Park", true },
            { "Dock", true },
            { "Power", true },
            { "RightHand", false },
            { "LeftHand", false }
        };
    }


    public void TurnButtonOn(string buttonName)
    {
        if (buttonLights.ContainsKey(buttonName))
        {
            buttonLights[buttonName].SetActive(true);
            buttonStates[buttonName] = true;
            if (buttonName == "Power")
            {
                NonPowerButtons.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("Button light not found: " + buttonName);
        }
    }

    public void TurnButtonOff(string buttonName)
    {
        if (buttonLights.ContainsKey(buttonName))
        {
            buttonLights[buttonName].SetActive(false);
            buttonStates[buttonName] = false;
            if (buttonName == "Power")
            {
                NonPowerButtons.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Button light not found: " + buttonName);
        }
    }
}