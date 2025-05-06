using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PatientStateManager : MonoBehaviour
{
    public GameObject patient;
    public PatientMenu patientMenu;
    public PatientMover patientMover;
    public List<PatientState> patientStates;
    public int initialState = 0;

    private GameObject patientModel;
    private Animator patientAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Get patient model and animator
        patientModel = patient.transform.GetChild(0).gameObject;
        patientAnimator = patientModel.GetComponent<Animator>();

        // Check if states exist
        if (patientStates.Count <= 0)
        {
            throw new System.Exception("Could not set initial patient state: No patient states have been defined.");
        }

        if (initialState < 0 || initialState >= patientStates.Count) 
        {
            throw new System.Exception("Could not set initial patient state: Initial patient state is out of range.");
        }

        // Set initial patient position
        ChangePatientState(patientStates[initialState]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePatientState(string newStateLabel)
    {
        PatientState newState = null;
        foreach (PatientState ps in patientStates)
        {
            if (ps.label.Equals(newStateLabel))
            {
                newState = ps;
            }
        }
        if (newState != null)
        {
            StartCoroutine(ChangePatientStateCoroutine(newState));
        }
        else
        {
            throw new System.Exception($"Could not change patient state: no match found for state '{newStateLabel}'");
        }
    }

    public void ChangePatientState(PatientState newState)
    {
        StartCoroutine(ChangePatientStateCoroutine(newState));
    }

    private IEnumerator ChangePatientStateCoroutine(PatientState newState)
    {
        patientMenu.Disable();

        // TODO: SOMETHING TO TEMPORARILY HANDLE MISSING STATES?
        // TODO: HANDLE HEADPHONE CHANGE?

        patientAnimator.enabled = false;
        patient.transform.Rotate(0, newState.options.pivotDegrees, 0);

        if (newState.transition.movementLabel != null && newState.transition.movementLabel != "") 
        {
            patientMover.SetCurrentMovement(newState.transition.movementLabel, ChangePatientState);
            patientMover.StartMoving();
        }

        if (newState.transition.animationName != null)
        {
            yield return StartCoroutine(PlayAnimation(newState.transition.animationName));
        }

        if (newState.transition.parent != null)
        {
            patient.transform.SetParent(newState.transition.parent.transform);
            if (newState.options.moveToParent) 
            {
                patient.transform.position = newState.transition.parent.transform.position;
            }
            
        }
        patient.transform.localPosition = new Vector3(
            patient.transform.localPosition.x + newState.options.xOffset,
            patient.transform.localPosition.y + newState.options.yOffset,
            patient.transform.localPosition.z + newState.options.zOffset
        );


        patientMenu.SetItems(newState.menuItems, ChangePatientState);
        patientMenu.Enable();
        patientMenu.ShowMenu();
    }

    private IEnumerator PlayAnimation(string animationName)
    {
        if (!patientAnimator.HasState(0, Animator.StringToHash(animationName)))
        {
            yield break;
        }
        patientAnimator.enabled = true;
        patientAnimator.Play(animationName, 0, 0f);
        patientAnimator.speed = 1;

        // Ensure animation has started
        yield return new WaitUntil(() => patientAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0);
        yield return new WaitForSeconds(patientAnimator.GetCurrentAnimatorStateInfo(0).length);
    }
}

[System.Serializable]
public class PatientState
{
    public string label;
    public List<string> menuItems;
    public StateBeginTransition transition;
    public StateOptions options;
}

[System.Serializable]
public class StateBeginTransition
{
    public string? animationName;
    public GameObject? parent;
    public string? movementLabel;
}

[System.Serializable]
public class StateOptions
{
    public float pivotDegrees;
    public bool moveToParent = false;
    public float xOffset;
    public float yOffset;
    public float zOffset;
    public bool changeStateImmediately = false;
}
