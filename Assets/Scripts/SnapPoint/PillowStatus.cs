using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowStatus : MonoBehaviour, CheckerInterface, RemovalInterface
{
    public bool isCorrect() {
        return transform.childCount != 0;
    }
    public bool isRemoved() {
        return !isCorrect();
    }
    public string getLabel() {
        return "Pillow Placement";
    }
    public string getRemovalLabel() {
        return "Pillow Removal";
    }
}
