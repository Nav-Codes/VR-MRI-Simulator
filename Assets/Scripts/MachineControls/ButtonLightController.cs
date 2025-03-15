using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controls the button light indicators for various buttons, including up, down, park, dock, power, and hand buttons.
/// This class inherits from <see cref="ButtonLightBase"/> and initializes button lights using a dictionary.
/// It manages the activation and deactivation of lights based on button states.
/// Additionally, it ensures that non-power buttons only activate when the power button is turned on.
/// </summary>
/// 
public class ButtonLightController : ButtonLightBase
{
    public GameObject upButtonLight;
    public GameObject downButtonLight;
    public GameObject parkButtonLight;
    public GameObject dockButtonLight;
    public GameObject powerButtonLight;
    public GameObject rightHandButtonLight;
    public GameObject leftHandButtonLight;
    public GameObject NonPowerButtons;
    public string[] buttonNames = { "Up", "Down", "Park", "Dock", "Power", "RightHand", "LeftHand" };
    private void Awake()
    {
        buttonLightObjects = new GameObject[] { upButtonLight, downButtonLight, parkButtonLight, dockButtonLight, powerButtonLight, rightHandButtonLight, leftHandButtonLight };
        InitializeLights(buttonNames);
    }


    public override void TurnButtonOn(string buttonName)
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

    public override void TurnButtonOff(string buttonName)
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