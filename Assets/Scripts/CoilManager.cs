using UnityEngine;
using TMPro;

public class CoilManager : MonoBehaviour
{
    public TMP_Dropdown coilDropdown; // Configure in Inspector

    [System.Serializable]
    public class CoilData
    {
        public string coilName;
        public GameObject coilPrefab; // The coil prefab
    }

    public CoilData[] coils; // Configure in Inspector
    private CoilData currentCoil;
    
    public void SpawnCoil()
    {
        if (currentCoil != null)
        {
            currentCoil.coilPrefab.SetActive(false);
        }

        CoilData selectedCoil = coils[coilDropdown.value];

        // Spawn bottom part on table
        if (selectedCoil.coilPrefab != null)
        {
            Debug.Log("Spawning " + selectedCoil.coilName);
            currentCoil = selectedCoil;
            selectedCoil.coilPrefab.SetActive(true);

            // GameObject coildata = Instantiate(selectedCoil.coilPrefab);
            // coildata.transform.SetActive(true);
     
        }
    }
}