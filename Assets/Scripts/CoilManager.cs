using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Required for TextMeshPro UI
using System.Collections.Generic;

public class CoilManager : MonoBehaviour
{
    public TMP_Dropdown CoilDropdown; // Configure in Inspector
    public GameObject PatientBed;
    public GameObject CoilObject;
    public GameObject CurrCoil = null;

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
            // Debug.Log(Coil.CoilPrefab.name);
            if (Coil.CoilPrefab != null && !CoilMap.ContainsKey(Coil.CoilName))
            {
                CoilMap.Add(Coil.CoilName, Coil.CoilPrefab);
                Coil.CoilPrefab.SetActive(true);
                //Ensures that all the top and bottom parts of the coils are visisble
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

        // Populate the dropdown with Coil names
        PopulateDropdown();
        StartCoroutine(CheckChange());
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

    public void SpawnCoilAttachPoint()
    {
        ResetCoils();
        // Debug.Log("coilName: " + CoilDropdown.options[CoilDropdown.value].text);

        string coilName = CoilDropdown.options[CoilDropdown.value].text;

        if (CoilMap.TryGetValue(coilName, out GameObject selectedCoilPrefab) && selectedCoilPrefab.transform.childCount >= 3)
        {
            CurrCoil = selectedCoilPrefab;
            selectedCoilPrefab.transform.SetParent(PatientBed.transform);
            foreach (Transform child in selectedCoilPrefab.transform)
            {
                if (child.name.ToLower().Contains("attach"))
                {
                    child.gameObject.SetActive(true);
                }
                // Debug.Log("child: " + child.name + " isActive: " + child.gameObject.activeSelf);
            }
        }
    }

    private void ResetCoils()
    {
        CurrCoil = null;
        foreach (var Coil in Coils)
        {
            Coil.CoilPrefab.transform.SetParent(CoilObject.transform);
            foreach (Transform child in Coil.CoilPrefab.transform)
            {
                if (child.name.ToLower().Contains("attach"))
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    /** This is to fix bug where red outline of coil still appears even though both coils are attached properly
    Only applies to coils with top and base parts (head, ankle, etc.) and assumes bottom coil is placed first */
    IEnumerator<object> CheckChange() 
    {
        bool coilsInRoot = false;
        while (true) 
        {
            yield return new WaitForSeconds(0.5f); // Adjust frequency as needed
            
            GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();

            for (int i = 0; i < rootObjects.Length; i++)
            {
                if (rootObjects[i].name.ToLower().Contains("coil") && rootObjects[i].name.ToLower().Contains("base"))
                {
                    for (int j = i; j < rootObjects.Length; j++)
                    {
                        if (rootObjects[j].name.ToLower().Contains("coil") && rootObjects[j].name.ToLower().Contains("top"))
                        {
                            coilsInRoot = true;
                            break;
                        }
                    }
                    if (coilsInRoot) break;
                    else coilsInRoot = false;
                }
            }

            if (CurrCoil == null) continue;

            //Gets the gameObject that the coils snap on to
            GameObject baseAttach = null;
            foreach (Transform child in CurrCoil.transform)
            {
                if (child.gameObject.name.ToLower().Contains("_base_attach"))
                {
                    baseAttach = child.gameObject;
                    break;
                }
            }

            //check if curr coil only has the attachPoint objects and if there is are top/bottom coil gameObjects under BedPlatform
            if (CurrCoil.transform.childCount == 3 && coilsInRoot)
            {
                baseAttach.GetComponent<BoxCollider>().enabled = false;
            } 
            else 
            {
                baseAttach.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
}
