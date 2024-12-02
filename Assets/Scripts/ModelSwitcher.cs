using UnityEngine;
using TMPro; // Required for TextMeshPro UI

public class ModelSwitcher : MonoBehaviour
{
    public GameObject[] patientModels; // Array of patient models
    public TMP_Dropdown dropdown; // Reference to the Dropdown

    void Start()
    {
        // Attach the OnDropdownValueChanged event to the dropdown
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    void OnDropdownValueChanged(int index)
    {
        // Disable all models
        foreach (GameObject model in patientModels)
        {
            model.SetActive(false);
        }
        // Enable the selected model
        if (index >= 0 && index < patientModels.Length)
        {
            patientModels[index].SetActive(true);
        }
    }
}
