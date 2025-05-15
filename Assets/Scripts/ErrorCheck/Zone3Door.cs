using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone3Door : MonoBehaviour, CheckerInterface
{
    private Animator animator;
    private bool isOpen = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }

    public bool isCorrect()
    {
        return !isOpen;
    }
    public string getLabel()
    {
        return "Zone 3 door";
    }
}
