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

    private bool isParkButtonOn = false;
    private bool isDockButtonOn = false;
    private bool isPowerButtonOn = false;
    private bool isRightHandButtonOn = false;
    private bool isLeftHandButtonOn = false;
    private Dictionary<string, GameObject> buttonLights;

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
    }


    public void TurnButtonOn(string buttonName)
    {
        if (buttonLights.ContainsKey(buttonName))
        {
            buttonLights[buttonName].SetActive(true);
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
        }
        else
        {
            Debug.LogWarning("Button light not found: " + buttonName);
        }
    }
}