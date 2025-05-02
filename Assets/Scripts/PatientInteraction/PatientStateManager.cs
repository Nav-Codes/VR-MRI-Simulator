using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientStateManager : MonoBehaviour
{
    public GameObject Patient;
    public PatientMenu PatientMenu;
    public List<PatientState> PatientStates;
    public int InitialState = 0;

    private GameObject PatientModel;
    private Animator PatientAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Get patient model and animator
        PatientModel = Patient.transform.GetChild(0).gameObject;
        PatientAnimator = PatientModel.GetComponent<Animator>();

        // CHECK IF ANY STATES EXIST?
        if (PatientStates.Count <= 0)
        {
            throw new System.Exception("No patient states have been defined.");
        }

        if (InitialState < 0 || InitialState >= PatientStates.Count) 
        {
            throw new System.Exception("Initial patient state is out of range.");
        }

        // Set initial patient position
        ChangePatientState(PatientStates[InitialState]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePatientState(string NewStateLabel)
    {
        PatientState NewState = null;
        foreach (PatientState ps in PatientStates)
        {
            if (ps.Label.Equals(NewStateLabel))
            {
                NewState = ps;
            }
        }
        if (NewState != null)
        {
            StartCoroutine(ChangePatientStateCoroutine(NewState));
        }
        else
        {
            Debug.Log($"Could not change patient state: no match found for state '{NewStateLabel}'");
        }
    }

    public void ChangePatientState(PatientState NewState)
    {
        StartCoroutine(ChangePatientStateCoroutine(NewState));
    }

    private IEnumerator ChangePatientStateCoroutine(PatientState NewState)
    {
        // CHECK THAT CORRECT STATE EXISTS?
        // DISABLE MENU

        // SOMETHING LIKE THIS TO TEMPORARILY HANDLE MISSING STATES?
        //if (!noAnimationPositions.Contains(selectedPositionName))
        //{
        //    ...
        //}

        // HANDLE HEADPHONE CHANGE?

        // FLIP MODEL IF NECESSARY
        // PLAY ANIMATION
        yield return StartCoroutine(PlayAnimation(NewState.Transition.AnimationName));

        // UPDATE PARENT
        // UPDATE POSITION

        // UPDATE MENU
        // ENABLE MENU
    }

    public IEnumerator PlayAnimation(string AnimationName)
    {
        

        // PLAY ANIMATION
        PatientAnimator.Play(AnimationName, 0, 0f);
        PatientAnimator.speed = 1;

        // Ensure animation has started
        yield return new WaitUntil(() => PatientAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0);
        yield return new WaitForSeconds(PatientAnimator.GetCurrentAnimatorStateInfo(0).length);
        PatientAnimator.speed = 0;
    }
}

[System.Serializable]
public class PatientState
{
    public string Label;
    public List<PatientMenuItem> MenuItems;
    public StateBeginTransition Transition;
}

[System.Serializable]
public class StateBeginTransition
{
    public string? AnimationName;
    public GameObject? Parent;
    public string? MovementLabel;
}
