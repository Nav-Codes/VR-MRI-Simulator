using UnityEngine;
using TMPro; // Required for TextMeshPro UI
using System;
using System.Collections;
using System.Collections.Generic;

public class StartScanManager : MonoBehaviour
{
    [SerializeField] private AudioSource scannerAudioSource;
    public ErrorCheck ErrorChecker;
    public GameObject Coils;
    public GameObject TissueObject;

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
        AddDirtyCoils();
    }

    private void AddDirtyCoils()
    {
        //iterate thru each of the coils
        foreach (Transform Coil in Coils.transform)
        {
            //iterate thru all the snap points on the coils 
            foreach (Transform SnapPoint in Coil.gameObject.transform)
            {
                //try getting a child gameObject from the snap point game object
                GameObject coilObject = null;

                foreach (Transform realCoil in SnapPoint.gameObject.transform)
                {
                    try
                    {
                        if (realCoil.gameObject.CompareTag("Coil"))
                        {
                            coilObject = realCoil.gameObject;
                        }
                    }
                    catch (UnityException) { continue; }
                }

                if (coilObject == null) continue;

                foreach (Transform child in coilObject.transform)
                {
                    if (child.gameObject.name.ToLower().Contains("smudge"))
                    {
                        TissueObject.GetComponent<Tissue>().AddDirtyObject(coilObject);
                    }
                }
            }
        }
    }
}