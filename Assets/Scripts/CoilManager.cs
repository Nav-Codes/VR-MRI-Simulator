using UnityEngine;
using TMPro; // Required for TextMeshPro UI
using System.Collections.Generic;

public class CoilManager : MonoBehaviour
{
    public TMP_Dropdown CoilDropdown; // Configure in Inspector

    [System.Serializable]
    public class CoilData
    {
        public string CoilName;
        public GameObject CoilPrefab; // Prefab for the Coil
    }

    // public GameObject[] Coils; 

    public CoilData[] Coils; // Configure in Inspector
    private Dictionary<string, GameObject> CoilMap; // For efficient lookup
    private GameObject activeCoil; // Reference to currently active Coil

    private void Awake()
    {
        // Initialize the dictionary
        CoilMap = new Dictionary<string, GameObject>();
        foreach (var Coil in Coils)
        {
            Debug.Log(Coil.CoilPrefab.name);
            if (Coil.CoilPrefab != null && !CoilMap.ContainsKey(Coil.CoilName))
            {
                CoilMap.Add(Coil.CoilName, Coil.CoilPrefab);
                Coil.CoilPrefab.SetActive(false); // Ensure all Coils are inactive at start

                //Ensures that all the top and bottom parts of the coils are visisble
                if (Coil.CoilPrefab.transform.childCount >= 3)
                {
                    Coil.CoilPrefab.SetActive(true);
                    foreach (Transform child in Coil.CoilPrefab.transform)
                    {
                        //Ensures that when the player is loaded in, all the snap on points are disabled until the user selects a scan type
                        if (child.name.ToLower().Contains("attach"))
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }

        // Populate the dropdown with Coil names
        PopulateDropdown();
    }

    private void PopulateDropdown()
    {
        CoilDropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (var Coil in Coils)
        {
            options.Add(Coil.CoilName);
        }
        CoilDropdown.AddOptions(options);
    }

    public void SpawnCoil()
    {

        // Disable the currently active Coil
        if (activeCoil != null)
        {
            activeCoil.SetActive(false);
        }

        // Get selected Coil name from dropdown
        string selectedCoilName = CoilDropdown.options[CoilDropdown.value].text;

        // Spawn and activate the selected Coil
        if (CoilMap.TryGetValue(selectedCoilName, out GameObject selectedCoilPrefab))
        {
            activeCoil = selectedCoilPrefab;
            activeCoil.SetActive(true);
            Debug.Log($"Activated Coil: {selectedCoilName}");
        }
        else
        {
            Debug.LogWarning($"Coil '{selectedCoilName}' not found in CoilMap.");
        }
    }

    public void SpawnFromParent()
    {
        Debug.Log("coilName: " + CoilDropdown.options[CoilDropdown.value].text);

        string coilName = CoilDropdown.options[CoilDropdown.value].text;

        if (CoilMap.TryGetValue(coilName, out GameObject selectedCoilPrefab) && selectedCoilPrefab.transform.childCount >= 3)
        {
            foreach (Transform child in selectedCoilPrefab.transform)
            {
                
                if (child.name.ToLower().Contains("attach"))
                {
                    child.gameObject.SetActive(true);
                }
                Debug.Log("child: " + child.name + " isActive: " + child.gameObject.activeSelf);
            }
        }
    }
}
