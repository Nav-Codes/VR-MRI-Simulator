using UnityEngine;
using TMPro; // Required for TextMeshPro UI
using System;
using System.Collections.Generic;

public class StartScanManager : MonoBehaviour
{
    [SerializeField] private AudioSource scannerAudioSource;
    public ErrorCheck ErrorChecker;
    public GameObject Coils;
    public Material Smudge;
    Tuple<GameObject, Material> Og1; // GameObject will be the coil, Material will be original material before smudging
    Tuple<GameObject, Material> Og2;

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

    private void SaveOgMaterials(GameObject Coil, Material[] materials)
    {
        if (Og1 == null)
        {
            Og1 = Tuple.Create(Coil, materials[0]);
        }
        else if (Og2 == null)
        {
            Og2 = Tuple.Create(Coil, materials[0]);
        }
    }
    
    public void RevertSmudge(GameObject Coil)
    {
        GameObject coilObject;
        Material ogMaterial;

        if (Og1.Item1.name.Equals(Coil.name))
        {
            coilObject = Og1.Item1;
            ogMaterial = Og1.Item2;
        }
        else if (Og2.Item1.name.Equals(Coil.name))
        {
            coilObject = Og2.Item1;
            ogMaterial = Og2.Item2;
        }
        else
        {
            return;
        }

        MeshRenderer meshRenderer = coilObject.GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            Material[] materials = meshRenderer.materials; // Get a copy of the materials array
            if (materials.Length > 0)
            {
                materials[0] = ogMaterial; // Replace the first material
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
                        SaveOgMaterials(coilObject, materials);
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