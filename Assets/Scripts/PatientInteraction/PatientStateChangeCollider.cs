using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientStateChangeCollider : MonoBehaviour
{
    public float triggerDistance = 0.2f; // Maximum distance for triggering state change
    public PatientStateManager patient;
    public GameObject expectedObject; // The specific object that can trigger state change
    public string targetStateLabel;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Expected {expectedObject}, got: {other.gameObject}");
        if (Vector3.Distance(expectedObject.transform.position, transform.position) < 5)
        {
            Debug.Log("In range");
            PatientState currentPatientState = patient.GetCurrentState();
            if (currentPatientState != null && currentPatientState.label != targetStateLabel)
            {
                patient.ChangePatientState(targetStateLabel);
            }
        }
        //if (other.gameObject == expectedObject) // Compare by reference
        //{
        //    Debug.Log("expected");
        //    PatientState currentPatientState = patient.GetCurrentState();
        //    if (currentPatientState != null && currentPatientState.label != targetStateLabel)
        //    {
        //        patient.ChangePatientState(targetStateLabel);
        //    }
        //    //TrySnapObject(other.transform);
        //}
    }
}
