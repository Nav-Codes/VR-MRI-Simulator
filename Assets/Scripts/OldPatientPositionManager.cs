using UnityEngine;
using TMPro;

public class OldPatientPositionManager : MonoBehaviour
{
    public TMP_Dropdown PatientPositionDropdown; // Configure in Inspector

    [System.Serializable]
    public class PatientPositionData
    {
        public string PatientPositionName;
        public GameObject PatientPositionPrefab; // The PatientPosition prefab
    }

    public PatientPositionData[] PatientPositions; // Configure in Inspector
    private PatientPositionData currentPatientPosition;
    
    public void SpawnPatientPosition()
    {
        if (currentPatientPosition != null)
        {
            currentPatientPosition.PatientPositionPrefab.SetActive(false);
        }

        PatientPositionData selectedPatientPosition = PatientPositions[PatientPositionDropdown.value];

        // Spawn bottom part on table
        if (selectedPatientPosition.PatientPositionPrefab != null)
        {
            Debug.Log("Spawning " + selectedPatientPosition.PatientPositionName);
            currentPatientPosition = selectedPatientPosition;
            selectedPatientPosition.PatientPositionPrefab.SetActive(true);

            // GameObject PatientPositiondata = Instantiate(selectedPatientPosition.PatientPositionPrefab);
            // PatientPositiondata.transform.SetActive(true);
     
        }
    }
}