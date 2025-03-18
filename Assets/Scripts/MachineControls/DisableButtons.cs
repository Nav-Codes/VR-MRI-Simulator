using UnityEngine;
/// <summary>
/// Toggles the active state of a set of buttons based on the active state of a trigger object.
/// </summary>
public class DisableButtons : MonoBehaviour
{
    public GameObject[] buttons;
    // if needed, use dummyButtons to replace the buttons when they are disabled
    public GameObject[] dummyButtons;
    public GameObject trigger;

    void Start()
    {
        SetButtonsActive(trigger.activeSelf);
    }

    void Update()
    {
        SetButtonsActive(trigger.activeSelf);
    }

    private void SetButtonsActive(bool isActive)
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(isActive);
        }
        foreach (GameObject dummyButton in dummyButtons)
        {
            dummyButton.SetActive(!isActive);
        }
    }
}
