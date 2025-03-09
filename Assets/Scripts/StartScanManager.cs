using UnityEngine;
using TMPro; // Required for TextMeshPro UI
using System.Collections.Generic;

public class StartScanManager : MonoBehaviour
{
    [SerializeField] private AudioSource scannerAudioSource;

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
        scannerAudioSource.Play();
    }
}