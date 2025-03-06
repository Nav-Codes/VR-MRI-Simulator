using UnityEngine;
using TMPro; // Required for TextMeshPro UI
using System.Collections.Generic;

public class PatientPositionManager : MonoBehaviour
{
    public TMP_Dropdown PatientPositionDropdown; // Configure in Inspector
    public GameObject PatientPositionMenu;

    [System.Serializable]
    public class PatientPositionData
    {
        public string PatientPositionName;
        public GameObject PatientPositionPrefab; // Prefab for the patient position
    }

    public PatientPositionData[] PatientPositions; // Configure in Inspector
    public GameObject DefaultPatientPosition;
    public GameObject OpenPositionMenuButton;
    private Dictionary<string, GameObject> PatientPositionMap; // Efficient lookup
    private GameObject activePatientPosition; // Currently active position
    private bool defaultEnabled = false;

    private void Awake()
    {
        // Initialize the dictionary
        PatientPositionMap = new Dictionary<string, GameObject>();
        foreach (var position in PatientPositions)
        {
            if (position.PatientPositionPrefab != null && !PatientPositionMap.ContainsKey(position.PatientPositionName))
            {
                PatientPositionMap.Add(position.PatientPositionName, position.PatientPositionPrefab);
                position.PatientPositionPrefab.SetActive(false); // Ensure all are inactive at start
            }
        }

        // Populate the dropdown with patient position names
        PopulateDropdown();
    }

    private void Update()
    {
        if (!defaultEnabled && PatientPositionDropdown.IsActive())
        {
            DefaultPatientPosition.SetActive(true);
            defaultEnabled = true;
        }
    }

    private void PopulateDropdown()
    {
        PatientPositionDropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (var position in PatientPositions)
        {
            options.Add(position.PatientPositionName);
        }
        PatientPositionDropdown.AddOptions(options);
    }

    public void SpawnPatientPosition()
    {
        // Disable the default patient position
        if (DefaultPatientPosition != null)
        {
            DefaultPatientPosition.SetActive(false);
        }

        // Disable the currently active patient position
        if (activePatientPosition != null)
        {
            activePatientPosition.SetActive(false);
        }

        // Get selected position name from dropdown
        string selectedPositionName = PatientPositionDropdown.options[PatientPositionDropdown.value].text;

        // Activate the selected position
        if (PatientPositionMap.TryGetValue(selectedPositionName, out GameObject selectedPositionPrefab))
        {
            activePatientPosition = selectedPositionPrefab;
            activePatientPosition.SetActive(true);
            Debug.Log($"Activated Patient Position: {selectedPositionName}");
            OpenPositionMenuButton.SetActive(true);
            PatientPositionMenu.SetActive(false);
            GameObject grandchild = FindChildByName(activePatientPosition.transform, "Headphone_Open")?.gameObject;
            if (grandchild != null)
            {
                grandchild.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning($"Patient Position '{selectedPositionName}' not found in PatientPositionMap.");
        }
    }

    public void ResetPositionMenu()
    {
        activePatientPosition?.SetActive(false);
        PatientPositionMenu.SetActive(true);
        OpenPositionMenuButton.SetActive(false);
        defaultEnabled = false;
    }
    private Transform FindChildByName(Transform parent, string name)
{
    foreach (Transform child in parent)
    {
        if (child.name == name)
            return child; // Found the child

        Transform found = FindChildByName(child, name); // Recursive search
        if (found != null)
            return found; // Return if found in deeper levels
    }
    return null; // Not found
}

}