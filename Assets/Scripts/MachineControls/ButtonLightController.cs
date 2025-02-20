using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLightController : MonoBehaviour
{
    public GameObject upButtonLight;
    public GameObject downButtonLight;
    public GameObject ParkButtonLight;
    public GameObject DockButtonLight;
    public GameObject powerButtonLight;
    public GameObject rightHandButtonLight;
    public GameObject leftHandButtonLight;

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
            { "Park", false },
            { "Dock", false },
            { "Power", false },
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
        }
        else
        {
            Debug.LogWarning("Button light not found: " + buttonName);
        }
    }
}