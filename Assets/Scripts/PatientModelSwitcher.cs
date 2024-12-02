using UnityEngine;

public class OldModelSwitcher : MonoBehaviour
{
    public GameObject[] patientModels; // Array of patient models

    public void SwitchModel(int index)
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
