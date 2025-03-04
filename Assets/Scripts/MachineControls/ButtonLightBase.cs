using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class for managing button lights. Provides common functionality to initialize, turn on, and turn off button lights.
/// Stores button states and their corresponding GameObjects in dictionaries.
/// Derived classes should initialize the buttonLights dictionary before calling InitializeLights().
/// </summary>
public abstract class ButtonLightBase : MonoBehaviour
{
    protected Dictionary<string, GameObject> buttonLights;
    protected Dictionary<string, bool> buttonStates;

    protected virtual void InitializeLights()
    {
        buttonStates = new Dictionary<string, bool>();
        foreach (var button in buttonLights)
        {
            buttonStates[button.Key] = button.Value.activeSelf;
        }
    }
    public virtual void TurnButtonOn(string buttonName) {
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
    public virtual void TurnButtonOff(string buttonName) {
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

