using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerDoorController : MonoBehaviour, CheckerInterface
{
    [SerializeField] private Animator myDoor = null;

    private bool isOpen = false;

    public void OpenDoor()
    {
        if (myDoor.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if (!isOpen)
            {
                myDoor.Play("DoorOpen", 0, 0.0f);
                isOpen = true;
            }

            else if (isOpen)
            {
                myDoor.Play("DoorClose", 0, 0.0f);
                isOpen = false;
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
