using UnityEngine;
using TMPro; // Required for TextMeshPro UI
using System.Collections.Generic;

public class StartScanManager : MonoBehaviour
{
    [SerializeField] private AudioSource scannerAudioSource;
    public ErrorCheck ErrorChecker;

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
        ErrorChecker.Check();
        scannerAudioSource.Play();
    }
}