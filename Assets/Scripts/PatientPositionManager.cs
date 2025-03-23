using UnityEngine;
using TMPro; // Required for TextMeshPro UI
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

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
    private string[] noAnimationPositions = {"Ankle", "Breast", "Hand (Flat)"};

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
            if (!noAnimationPositions.Contains(selectedPositionName))
            {
                AnimateLieDown();
                yield return new WaitForSeconds(3.292f); // Length of lie down animation
            }
                
            transitionModel.SetActive(false);
            activePatientPosition.SetActive(true);
            Debug.Log($"Activated Patient Position: {selectedPositionName}");
            
            GameObject grandchild = FindChildByName(activePatientPosition.transform, "Headphone_Open")?.gameObject;
            if (grandchild != null)
            {
                grandchild.gameObject.SetActive(false);
            }

            if (!noAnimationPositions.Contains(selectedPositionName))
            {
                yield return new WaitForSeconds(2);
                AnimateCoilAccomodation();
                yield return new WaitForSeconds(18.0f / 24.0f);
                transitionAnimator.speed = 0;
            }

            OpenPositionMenuButton.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Patient Position '{selectedPositionName}' not found in PatientPositionMap.");
        }
    }

    public void AnimateLieDown()
    {
        if (transitionModel != null)
        {
            transitionModel.SetActive(true);
        }
        transitionAnimator.Play("90-0_Transition", 0, 0f);
        transitionAnimator.speed = 1;
    }

    public void AnimateSitUp()
    {
        if (transitionModel != null)
        {
            transitionModel.SetActive(true);
        }
        transitionAnimator.Play("0-90_Transition", 0, 0f);
        transitionAnimator.speed = 1;
    }

    public void AnimateCoilAccomodation()
    {
        activePatientPosition.SetActive(false);
        if (transitionModel != null)
        {
            transitionModel.SetActive(true);
        }
        transitionAnimator.Play("0-KneeRaise_Transition", 0, 0f);
    }

    public void ResetPositionMenu()
    {
        StartCoroutine(ResetPositionMenuCoroutine());
    }

    public IEnumerator ResetPositionMenuCoroutine()
    {
        activePatientPosition?.SetActive(false);
        AnimateSitUp();
        yield return new WaitUntil(() => transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0);
        yield return new WaitForSeconds(transitionAnimator.GetCurrentAnimatorStateInfo(0).length);
        transitionModel.SetActive(false);
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