using UnityEngine;
using TMPro; // Required for TextMeshPro UI
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class StartScanManager : MonoBehaviour
{
    [SerializeField] private AudioSource scannerAudioSource;
    public ErrorCheck ErrorChecker;
    public GameObject Coils;
    public GameObject TissueObject;
    
    [SerializeField] private Transform panel2ToCopy;  // Source panel to copy from 
    [SerializeField] private Transform Final2ndPanel; // Destination panel to copy to
    [SerializeField] private GameObject ErrorTextPrefab; 
    
    
    private bool IsErrorText(GameObject obj)
    {
        // Check if this is the specific error text we want to ignore
        TMP_Text textComponent = obj.GetComponent<TMP_Text>();
        if (textComponent == null) return false;
    
        return textComponent.text.Contains("Procedure Feedback") ||
               textComponent.text.Contains("Please fix your errors before continuing");
    }
    
    private GameObject AddText(string text, Transform Panel,Color color, bool isTitle = false)
    {
        GameObject errorTextObj = Instantiate(ErrorTextPrefab, Panel);
        LayoutRebuilder.ForceRebuildLayoutImmediate(Panel.GetComponent<RectTransform>());

        TMP_Text errorText = errorTextObj.GetComponent<TMP_Text>();
        if (errorText == null)
        {
            throw new Exception("ErrorTextPrefab missing TMP_Text component");
        }

        errorText.text = isTitle ? $"<style=\"Title\">{text}</style>" : text;
        errorText.color = color;

        return errorTextObj;
    }

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
        StartCoroutine(WaitForAudioToEnd());

        CopySecondDisplay();
    }
    
    private void CopySecondDisplay()
    {
        if (panel2ToCopy == null || Final2ndPanel == null)
        {
            Debug.LogError("First panel references are missing!");
            return;
        }

        // Clear existing children in destination
        foreach (Transform child in Final2ndPanel)
        {
            Destroy(child.gameObject);
        }

        AddText("Second Check Results",Final2ndPanel, Color.black, true);
        
        foreach (Transform child in panel2ToCopy)
        {
            // Copy all children from source to destination
            if (!IsErrorText(child.gameObject))
            {
                GameObject newChild = Instantiate(child.gameObject, Final2ndPanel);
            }

            Debug.Log($"Copied children from {panel2ToCopy.name} to {Final2ndPanel.name}");
        }
    }

    private IEnumerator WaitForAudioToEnd()
    {
        while (scannerAudioSource.isPlaying)
        {
            yield return new WaitForSeconds(1f); // Wait 1 second between checks
        }
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
                        child.gameObject.SetActive(true);
                        TissueObject.GetComponent<Tissue>().AddDirtyCoil(coilObject);
                    }
                }
            }
        }
    }
}