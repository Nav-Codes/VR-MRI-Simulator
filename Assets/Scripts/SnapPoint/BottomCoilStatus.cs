using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomCoilStatus : MonoBehaviour, CheckerInterface
{
    public bool isCorrect()
    {
        return transform.childCount > 0;
    }

    public string getLabel()
    {
        return "Coil Bottom Selection";
    }
}
