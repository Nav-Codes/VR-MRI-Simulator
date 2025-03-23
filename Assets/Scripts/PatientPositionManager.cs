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
            //DefaultPatientPosition.SetActive(true);
            StartCoroutine(SetInitialTransitionModelPosition());
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
                //FlipModel();
                yield return StartCoroutine(PlayAnimation("Bedside-90", 2.6f));
                FlipModel();
                yield return StartCoroutine(PlayAnimation("90-0_Transition"));
            }
            
            Debug.Log($"Activated Patient Position: {selectedPositionName}");
            
            GameObject grandchild = FindChildByName(activePatientPosition.transform, "Headphone_Open")?.gameObject;
            if (grandchild != null)
            {
                grandchild.gameObject.SetActive(false);
            }

            if (!noAnimationPositions.Contains(selectedPositionName))
            {
                yield return new WaitForSeconds(2);
                yield return StartCoroutine(PlayAnimation("0-KneeRaise_Transition", 18.0f / 24.0f));
                
            }
            OpenPositionMenuButton.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Patient Position '{selectedPositionName}' not found in PatientPositionMap.");
        }
    }

    public IEnumerator PlayAnimation(string animationName, float? animTime = null)
    {
        if (transitionModel != null)
        {
            transitionModel.SetActive(true);
        }
        transitionAnimator.Play(animationName, 0, 0f);
        transitionAnimator.speed = 1;
        yield return new WaitUntil(() => transitionAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0);
        if (animTime != null) 
        {
            yield return new WaitForSeconds((float)animTime);
        } else
        {
            yield return new WaitForSeconds(transitionAnimator.GetCurrentAnimatorStateInfo(0).length);
        }
        transitionAnimator.speed = 0;
    }

    public void ResetPositionMenu()
    {
        StartCoroutine(ResetPositionMenuCoroutine());
    }

    public IEnumerator ResetPositionMenuCoroutine()
    {
        OpenPositionMenuButton.SetActive(false);
        yield return StartCoroutine(PlayAnimation("0-90_Transition"));
        FlipModel();
        yield return StartCoroutine(PlayAnimation("90-Bedside", 2.6f));
        FlipModel();
        PatientPositionMenu.SetActive(true);
        defaultEnabled = false;
    }

    public IEnumerator SetInitialTransitionModelPosition()
    {
        FlipModel();
        yield return StartCoroutine(PlayAnimation("Bedside-90", 0));
    }

    public void FlipModel()
    {
        transitionModel.transform.localScale = new Vector3
                (
                    transitionModel.transform.localScale.x,
                    transitionModel.transform.localScale.y,
                    -transitionModel.transform.localScale.z
                );
        if (transitionModel.transform.localScale.z < 0)
        {
            transitionModel.transform.position = new Vector3(0, 0, 0.23f);
        }
        else
        {
            transitionModel.transform.position = new Vector3(0, 0, 0);
        }
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