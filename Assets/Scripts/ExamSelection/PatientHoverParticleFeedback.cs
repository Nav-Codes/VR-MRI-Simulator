using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class PatientHoverParticleFeedback : XRBaseInteractable
{
    public PatientStateManager patient;
    public string examSelectionPatientStateLabel;
    public DataBanker databanker;
    public string examType;
    public ParticleSystem hoverParticles;
    public InputActionReference rightActivateAction;
    public InputActionReference leftActivateAction;

    private int hoverCount = 0;
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
            databanker.SetExamType(examType);
        }
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        isHovered = true;
        HandleHoverEntered();
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        isHovered = false;
        HandleHoverExited();
    }

    private void HandleHoverEntered()
    {
        PatientState patientState = patient.GetCurrentState();
        if (patientState == null || patientState.label != examSelectionPatientStateLabel) return;

        hoverCount++;
        if (hoverParticles != null && !hoverParticles.isPlaying)
            hoverParticles.Play();
    }

    private void HandleHoverExited()
    {
        hoverCount = Mathf.Max(hoverCount - 1, 0);
        if (hoverCount == 0 && hoverParticles != null && hoverParticles.isPlaying)
            hoverParticles.Stop();
    }
}
