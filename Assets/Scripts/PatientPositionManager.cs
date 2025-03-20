using UnityEngine;
using TMPro; // Required for TextMeshPro UI
using System.Collections.Generic;
using System.Collections;

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
    public CoilManager coilManager;
    public GameObject transitionModel;
    public Animator transitionAnimator;

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
        StartCoroutine(SpawnPatientPositionCoroutine());
    }

    private IEnumerator SpawnPatientPositionCoroutine()
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
            PatientPositionMenu.SetActive(false);
            if (selectedPositionName == "Knee")
            {
                AnimateTransition();
                yield return new WaitForSeconds(3);
            }
                
            transitionModel.SetActive(false);
            activePatientPosition.SetActive(true);
            Debug.Log($"Activated Patient Position: {selectedPositionName}");
            OpenPositionMenuButton.SetActive(true);
            
            GameObject grandchild = FindChildByName(activePatientPosition.transform, "Headphone_Open")?.gameObject;
            if (grandchild != null)
            {
                grandchild.gameObject.SetActive(false);
            }

            if (selectedPositionName == "Knee")
            {
                yield return new WaitForSeconds(2);
                AnimateCoilAccomodation();
                yield return new WaitForSeconds(18.0f / 24.0f);
                transitionAnimator.speed = 0;
            }
                
        }
        else
        {
            Debug.LogWarning($"Patient Position '{selectedPositionName}' not found in PatientPositionMap.");
        }
    }

    public void AnimateTransition()
    {
        string selectedPositionName = PatientPositionDropdown.options[PatientPositionDropdown.value].text;
        if (selectedPositionName != "Knee")
        {
            return;
        }
        if (transitionModel != null)
        {
            transitionModel.SetActive(true);
        }
        transitionAnimator.Play("90-0_Transition", 0, 0f);
        //while (transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("90-0_Transition")
        //    && transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        //if (transitionModel != null)
        //{
        //    transitionModel.SetActive(false);
        //}
    }

    public void AnimateCoilAccomodation()
    {
        string selectedPositionName = PatientPositionDropdown.options[PatientPositionDropdown.value].text;
        if (selectedPositionName != "Knee")
        {
            return;
        }
        activePatientPosition.SetActive(false);
        if (transitionModel != null)
        {
            transitionModel.SetActive(true);
        }
        transitionAnimator.Play("0-KneeRaise_Transition", 0, 0f);
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