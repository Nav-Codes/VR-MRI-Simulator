using UnityEngine;

public class TouchscreenMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject comfortMenuPanel;  // Panel for the Comfort menu
    [SerializeField] private GameObject physioMenuPanel;   // Panel for the Physio menu
    [SerializeField] private GameObject settingsMenuPanel; // Panel for the Settings menu
    [SerializeField] private GameObject patientPanel;
    [SerializeField] private GameObject sidePanel;

    void Start()
    {
        patientPanel.SetActive(true);
        sidePanel.SetActive(true);
    }

    // Show the Comfort menu and hide others
    public void ShowComfortMenu()
    {
        comfortMenuPanel.SetActive(true);
        physioMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
        patientPanel.SetActive(false);
    }

    // Show the Physio menu and hide others
    public void ShowPhysioMenu()
    {
        comfortMenuPanel.SetActive(false);
        physioMenuPanel.SetActive(true);
        settingsMenuPanel.SetActive(false);
        patientPanel.SetActive(false);
    }

    // Show the Settings menu and hide others
    public void ShowSettingsMenu()
    {
        comfortMenuPanel.SetActive(false);
        physioMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
        patientPanel.SetActive(false);
    }

    public void TogglePatientPanel()
    {
        if (patientPanel != null)
        {
            patientPanel.SetActive(!patientPanel.activeSelf);
            sidePanel.SetActive(!sidePanel.activeSelf);
        }
    }

}
