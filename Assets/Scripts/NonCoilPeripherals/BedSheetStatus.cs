using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedSheetStatus : MonoBehaviour, CheckerInterface
{
    public GameObject BedSheet; // Assign in Inspector
    public bool isCorrect() {
        return BedSheet.GetComponent<MeshRenderer>().enabled;   
    }
    public string getLabel() {
        return "Bed Sheet Placement";
    }
}
