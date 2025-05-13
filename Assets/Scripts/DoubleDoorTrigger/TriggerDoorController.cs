using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerDoorController : MonoBehaviour, CheckerInterface
{
    [SerializeField] private Animator leftDoor = null;
    [SerializeField] private Animator rightDoor = null;
    private bool isOpen = false;

    public void OpenDoor()
    {
        if (leftDoor.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            //Transform parent = door.parent;
            if (leftDoor != null && rightDoor != null)
            {
                if (!isOpen)
                {
                    // Opening the door
                    leftDoor.Play("DoorOpenClockwise", 0, 0.0f);
                    rightDoor.Play("DoorOpenCounterclockwise", 0, 0.0f);

                    isOpen = true;
                }
                else
                {
                    // Closing the door
                    leftDoor.Play("DoorCloseCounterclockwise", 0, 0.0f);
                    rightDoor.Play("DoorCloseClockwise", 0, 0.0f);

                    isOpen = false;
                }
            }
        }
    }

    public string getLabel()
    {
        return "Door closed";
    }

    public bool isCorrect()
    {
        return !isOpen;
    }
}
