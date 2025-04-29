using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowStatus : MonoBehaviour, CheckerInterface
{
    public GameObject Pillow; // Assign in Inspector
    public bool isCorrect() {
        return Pillow.GetComponent<MeshRenderer>().enabled;   
    }
    public string getLabel() {
        return "Pillow Placement";
    }
}
