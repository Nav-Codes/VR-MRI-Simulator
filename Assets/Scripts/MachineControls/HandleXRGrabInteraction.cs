using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandleXRGrabInteraction : XRGrabInteractable
{
    public ButtonLightController buttonLightController;
    public string buttonName; // The name of the button light to control

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (buttonLightController != null)
        {
            buttonLightController.TurnButtonOn(buttonName);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (buttonLightController != null)
        {
            buttonLightController.TurnButtonOff(buttonName);
        }
    }
}
