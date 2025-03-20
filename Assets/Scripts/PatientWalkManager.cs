using UnityEngine;
using TMPro; // Required for TextMeshPro UI
using System.Collections.Generic;
using TMPro.Examples;

public class PatientWalkManager : MonoBehaviour
{
    public GameObject walkingPatient = null;
    public GameObject seatedPatient = null;
    public Animator patientAnimator = null;
    public GameObject callInMenu = null;
    public GameObject patientPositionMenu = null;
    private bool isWalking1 = false;
    private bool isWalking2 = false;
    private bool isTurning1= false;
    private bool isTurning2 = false;
    private bool walkFinished = false;
    private Vector3 startPos = new Vector3(7.5f, 0f, -1.9f);
    private Vector3 turnPos = new Vector3(5f, 0f, -1.9f);
    private Vector3 endPos = new Vector3(0.95f, 0f, -0.65f);
    private float frameCount = 0;
    private float nWalkFrames1 = 800;
    private float nWalkFrames2 = 1600;
    private float nTurnFrames1 = 30;
    private float nTurnFrames2 = 180;
    private float walkProgress = 0;

    void Update()
    {
        if (isWalking1)
        {
            walkProgress = frameCount / nWalkFrames1;
            
            walkingPatient.transform.position = Vector3.Lerp(startPos, turnPos, walkProgress);
            frameCount++;
            if (walkProgress > 1)
            {
                isWalking1 = false;
                isTurning1 = true;
                frameCount = 0;
            }
        }

        if (isTurning1)
        {
            walkProgress = frameCount / nTurnFrames1;
            Debug.Log(walkProgress);
            walkingPatient.transform.Rotate(0f, 10f / nTurnFrames1, 0f, Space.Self);
            frameCount++;
            if (walkProgress > 1)
            {
                isTurning1 = false;
                isWalking2 = true;
                frameCount = 0;
            }
        }

        if (isWalking2)
        {
            walkProgress = frameCount / nWalkFrames2;
            Debug.Log("walking");
            walkingPatient.transform.position = Vector3.Lerp(turnPos, endPos, walkProgress);
            frameCount++;
            if (walkProgress > 1)
            {
                isWalking2 = false;
                isTurning2 = true;
                frameCount = 0;
            }
        }

        if (isTurning2)
        {
            walkProgress = frameCount / nTurnFrames2;
            Debug.Log(walkProgress);
            walkingPatient.transform.Rotate(0f, -100f / nTurnFrames2, 0f, Space.Self);
            frameCount++;
            if (walkProgress > 1)
            {
                isTurning2 = false;
                SeatPatient();
            }
        }
    }

    public void CallPatientIn()
    {
        patientAnimator.Play("WalkCycle", 0, 0.0f);
        isWalking1 = true;
        callInMenu.SetActive(false);
    }

    public void SeatPatient()
    {
        walkingPatient.SetActive(false);
        patientPositionMenu.SetActive(true);
    }
}