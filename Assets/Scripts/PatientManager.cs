using UnityEngine;
using TMPro; // Required for TextMeshPro UI
using System.Collections.Generic;

public class PatientManager : MonoBehaviour
{
    public GameObject patient = null;
    public Animator patientAnimator = null;
    public GameObject callInMenu = null;
    public void CallPatientIn()
    {
        patientAnimator.Play("PatientWalk", 0, 0.0f);
        callInMenu.SetActive(false);
    }
}