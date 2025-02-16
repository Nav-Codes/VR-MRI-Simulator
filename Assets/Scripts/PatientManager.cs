using UnityEngine;
using TMPro; // Required for TextMeshPro UI
using System.Collections.Generic;

public class PatientManager : MonoBehaviour
{
    public GameObject walkingPatient = null;
    public GameObject seatedPatient = null;
    public Animator patientAnimator = null;
    public GameObject callInMenu = null;
    public GameObject patientPositionMenu = null;
    private bool walkFinished = false;

    void Update()
    {
        if (!walkFinished && patientAnimator.GetCurrentAnimatorStateInfo(0).IsName("PatientWalk")
            && patientAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            walkFinished = true;
            SeatPatient();
        }
    }

    public void CallPatientIn()
    {
        patientAnimator.Play("PatientWalk", 0, 0.0f);
        callInMenu.SetActive(false);
    }

    public void SeatPatient()
    {
        walkingPatient.SetActive(false);
        patientPositionMenu.SetActive(true);
    }
}