using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedSheetStatus : MonoBehaviour, CheckerInterface, ReturnedInterface
{
    public GameObject OpenBedSheet; // Assign in Inspector
    public bool isCorrect() {
        return OpenBedSheet.activeSelf;   
    }
    public string getLabel() {
        return "Bed Sheet Placement";
    }

    public bool isReturned() {
        return !isCorrect();   
    }
    public string getReturnedLabel() {
        return "Bed Sheet";
    }
}
