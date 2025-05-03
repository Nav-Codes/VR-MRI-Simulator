using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedSheetStatus : MonoBehaviour, CheckerInterface
{
    public GameObject OpenBedSheet; // Assign in Inspector
    public bool isCorrect() {
        return OpenBedSheet.activeSelf;   
    }
    public string getLabel() {
        return "Bed Sheet Placement";
    }
}
