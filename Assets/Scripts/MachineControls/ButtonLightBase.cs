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
    protected GameObject[] buttonLightObjects;
    protected Dictionary<string, GameObject> buttonLights;
    protected Dictionary<string, bool> buttonStates;

    protected virtual void InitializeLights(string[] buttonNames)
    {
        if (buttonNames.Length != buttonLightObjects.Length)
        {
            Debug.LogError("Button names and button light objects arrays do not match in length!");
            return;
        }
        
        buttonLights = new Dictionary<string, GameObject>();
        for (int i = 0; i < buttonNames.Length; i++)
        {
            buttonLights.Add(buttonNames[i], buttonLightObjects[i]);
        }
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

