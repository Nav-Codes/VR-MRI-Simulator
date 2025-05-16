using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientWalkIn : MonoBehaviour
{
    public GameObject RightCurtainController;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.ToLower().Contains("snappoint"))
            RightCurtainController.GetComponent<SmoothCurtainAnimator>().TriggerAnimation();
    }
}
