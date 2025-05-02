using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowStatus : MonoBehaviour, CheckerInterface
{
    public bool isCorrect() {
        return (transform.GetChild(0).childCount > 0); 
    }
    public string getLabel() {
        return "Pillow Placement";
    }
}
