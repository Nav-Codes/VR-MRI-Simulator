using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
public class PatientHoverParticleFeedback : XRBaseInteractable
{
    public PatientStateManager patient;
    public string examSelectionPatientStateLabel;
    public DataBanker databanker;
    public string examType;
    public ParticleSystem hoverParticles;
    private int hoverCount = 0;

    void Start()
    {
        if (hoverParticles != null)
            hoverParticles.Stop(); // Ensure the particles are not playing at the start
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        HoverEnteredLogic();
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        HoverExitedLogic();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        PatientState patientState = patient.GetCurrentState();
        if (patientState == null || patientState.label != examSelectionPatientStateLabel) return;

        base.OnSelectEntered(args);
        databanker.SetExamType(examType);
    }

    private void HoverEnteredLogic()
    {
        PatientState patientState = patient.GetCurrentState();
        if (patientState == null || patientState.label != examSelectionPatientStateLabel) return;

        hoverCount++;

        if (hoverParticles != null && !hoverParticles.isPlaying)
            hoverParticles.Play();
    }

    private void HoverExitedLogic()
    {
        hoverCount = Mathf.Max(hoverCount - 1, 0);

        if (hoverCount == 0 && hoverParticles != null && hoverParticles.isPlaying)
            hoverParticles.Stop();
    }
}
