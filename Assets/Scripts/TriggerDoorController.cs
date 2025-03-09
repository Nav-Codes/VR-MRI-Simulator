using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

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
}
