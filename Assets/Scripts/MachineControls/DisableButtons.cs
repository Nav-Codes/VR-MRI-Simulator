using UnityEngine;
/// <summary>
/// Toggles the active state of a set of buttons based on the active state of a trigger object.
/// </summary>
public class DisableButtons : MonoBehaviour
{
    public GameObject[] buttons;
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
    }
}
