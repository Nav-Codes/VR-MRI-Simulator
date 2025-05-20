using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientWalkIn : MonoBehaviour
{
    public GameObject RightCurtainController;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.ToLower().Contains("headtarget"))
            RightCurtainController.GetComponent<SmoothCurtainAnimator>().TriggerAnimation();
    }
}
