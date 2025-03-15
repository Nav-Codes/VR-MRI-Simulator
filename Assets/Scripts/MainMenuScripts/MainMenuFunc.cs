using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // For TextMeshPro support
using UnityEngine.EventSystems; // Required for EventSystem
using Unity.XR.CoreUtils;
using UnityEngine.InputSystem.XR; // Required for XR Input Reset
using UnityEngine.XR.Management;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

public class MainMenuFunc : MonoBehaviour
{
    public XROrigin currentXROrigin; // Assign in Inspector
    public GameObject canvas1; // Assign in Inspector
    public GameObject canvas2; // Assign in Inspector

    private void Awake()
    {
        // Any initialization can go here
    }

    public void SwitchCanvas()
    {
        Invoke(nameof(ToggleCanvas), 0.1f); // Small delay to avoid UI glitches
    }

    private void ToggleCanvas()
    {
        canvas1.SetActive(!canvas1.activeSelf);
        canvas2.SetActive(!canvas2.activeSelf);
    }

    public void OnButtonClicked(Button clickedButton)
    {
        if (clickedButton == null)
        {
            Debug.LogError("No button was clicked!");
            return;
        }

        Debug.Log("Button Clicked: " + clickedButton.name);

        TextMeshProUGUI buttonTextComponent = clickedButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonTextComponent == null)
        {
            Debug.LogError("TextMeshProUGUI component not found in button children!");
            return;
        }

        string buttonText = buttonTextComponent.text;
        Debug.Log("Button Text: " + buttonText);

        if (buttonText == "Male" || buttonText == "Female")
        {
            DataBanker.Instance.SetSex(buttonText);
            SwitchCanvas();
        }
        else
        {
            examCheck(buttonText);
        }
    }

    public void examCheck(string check)
    {
        Debug.Log("Exam Check called with: " + check);
        DataBanker.Instance.SetExamType(check);

        // Teleport the current XR Rig to position (0, 0, 0)
        TeleportCurrentXRToOrigin();
    }

    private void TeleportCurrentXRToOrigin()
    {
        if (currentXROrigin != null)
        {
            // Teleport the XR Origin to (0, 0, 0)
            currentXROrigin.transform.position = new Vector3(5, 0, 0);
            Debug.Log("XR Rig teleported to (0, 0, 0)");

			currentXROrigin.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            Debug.LogError("Current XROrigin not found for teleportation!");
        }
    }
}