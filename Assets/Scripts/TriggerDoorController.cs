using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerDoorController : MonoBehaviour, CheckerInterface
{
    [SerializeField] private Animator myDoor = null;
    
    [SerializeField] private DataBanker DataBanker = null;
    
    [SerializeField] private ErrorCheck firstErrorCheck = null;

    private bool isOpen = false;

    private void doNothing()
    {
        Debug.Log("doNothing");
    }
    
    public void afterFirstOpen()
    {
        if (DataBanker != null && DataBanker.Instance.isFirstCheck() == false)
        {
            DataBanker.setFirstCheck(true);
            firstErrorCheck.Check(OpenDoor, doNothing);
        }
        else
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {

        if (myDoor.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Transform parent = transform.parent;
            if (parent != null)
            {
                string parentName = parent.name;

                if (!isOpen)
                {
                    // Opening the door
                    if (parentName == "LeftDoor")
                    {
                        myDoor.Play("Open", 0, 0.0f);
                    }
                    else if (parentName == "RightDoor")
                    {
                        myDoor.Play("DoorOpen", 0, 0.0f);
                    }

                    isOpen = true;
                }
                else
                {
                    // Closing the door
                    if (parentName == "LeftDoor")
                    {
                        myDoor.Play("Close", 0, 0.0f);
                    }
                    else if (parentName == "RightDoor")
                    {
                        myDoor.Play("DoorClose", 0, 0.0f);
                    }

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
