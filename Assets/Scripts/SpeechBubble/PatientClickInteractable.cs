using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PatientClickInteractable : XRBaseInteractable
{
    public InputActionReference rightActivateAction;
    public InputActionReference leftActivateAction;
    public UnityEvent TriggerEvent;
    private bool isHovered = false;

    void Start()
    {
        if (rightActivateAction != null)
        {
            rightActivateAction.action.Enable();
            rightActivateAction.action.performed += OnActivatePressed;
        }

        if (leftActivateAction != null)
        {
            leftActivateAction.action.Enable();
            leftActivateAction.action.performed += OnActivatePressed;
        }
    }


    void OnDisable()
    {
        if (rightActivateAction != null)
            rightActivateAction.action.performed -= OnActivatePressed;

        if (leftActivateAction != null)
            leftActivateAction.action.performed -= OnActivatePressed;
    }


    private void OnActivatePressed(InputAction.CallbackContext context)
    {
        if (isHovered)
        {
            TriggerEvent.Invoke();
        }
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        isHovered = true;
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        isHovered = false;
    }
}
