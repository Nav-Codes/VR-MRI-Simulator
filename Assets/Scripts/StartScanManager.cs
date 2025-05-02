using UnityEngine;
using TMPro; // Required for TextMeshPro UI
using System.Collections.Generic;

// to find the coil that is on the table, check the Coils game object if 

public class StartScanManager : MonoBehaviour
{
    [SerializeField] private AudioSource scannerAudioSource;
    public ErrorCheck ErrorChecker;
    public GameObject Coils;
    public Material Smudge;

    private void Start()
    {
        // Ensure scanner sound is stopped initially
        if (scannerAudioSource != null)
        {
            //scannerAudioSource.loop = true;
            scannerAudioSource.Stop();
        }
    }
    public void StartScan()
    {
        ErrorChecker.Check(OnContinueClick, () => { });
    }

    public void OnContinueClick()
    {
        scannerAudioSource.Play();
        ApplySmudge();
    }

    private void ApplySmudge()
    {
        //iterate thru each of the coils
        foreach (Transform Coil in Coils.transform)
        {
            //iterate thru all the snap points on the coils 
            foreach (Transform SnapPoint in Coil.gameObject.transform)
            {
                //try getting a child gameObject from the snap point game object
                GameObject coilObject;
                try
                {
                    coilObject = SnapPoint.gameObject.transform.GetChild(0).gameObject;
                }
                catch (UnityException e) { continue; }

                MeshRenderer meshRenderer = coilObject.GetComponent<MeshRenderer>();

                if (meshRenderer != null)
                {
                    Material[] materials = meshRenderer.materials; // Get a copy of the materials array
                    if (materials.Length > 0)
                    {
                        materials[0] = Smudge; // Replace the first material
                        meshRenderer.materials = materials; // Reassign the modified array
                    }
                    else
                    {
                        Debug.LogWarning("No materials found on the MeshRenderer.");
                    }
                }
                else
                {
                    Debug.LogWarning("No MeshRenderer found on the GameObject.");
                }
            }
        }
    }

}