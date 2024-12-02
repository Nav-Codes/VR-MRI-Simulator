using UnityEngine;
using TMPro; // Use this if you're working with TextMeshPro Dropdowns

public class CoilMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdownMenu; // Reference to the dropdown menu
    [SerializeField] private GameObject confirmButton; // Reference to the Confirm button
    [SerializeField] private GameObject resetButton;   // Reference to the Reset button

    private bool isCoilInInitialPosition = true; // Temporary placeholder for coil position logic

    private void Start()
    {
        // Add listener to detect when the dropdown value changes
        if (dropdownMenu != null)
        {
            dropdownMenu.onValueChanged.AddListener(OnDropdownChanged);
        }
    }

    // Called when the dropdown value changes
    private void OnDropdownChanged(int newValue)
    {
        UpdateButtons(true); // Pass 'true' since the dropdown value has changed
    }

    // Update the button visibility based on coil position and dropdown state
    public void UpdateButtons(bool hasDropdownChanged)
    {
        // Logic placeholder: If the coil is no longer in the initial position, show the Reset button
        if (!isCoilInInitialPosition)
        {
            ShowResetButton();
        }
        // If the dropdown value has changed or no coil is present, show theh Confirm button
        else if (hasDropdownChanged || !isCoilInInitialPosition)
        {
            ShowConfirmButton();
        }
    }

    // Show the Confirm button and hide the Reset button
    private void ShowConfirmButton()
    {
        if (confirmButton != null && resetButton != null)
        {
            confirmButton.SetActive(true);
            resetButton.SetActive(false);
        }
    }

    // Show the Reset button and hide the Confirm button
    private void ShowResetButton()
    {
        if (confirmButton != null && resetButton != null)
        {
            confirmButton.SetActive(false);
            resetButton.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        // Remove listener to avoid memory leaks
        if (dropdownMenu != null)
        {
            dropdownMenu.onValueChanged.RemoveListener(OnDropdownChanged);
        }
    }
}
