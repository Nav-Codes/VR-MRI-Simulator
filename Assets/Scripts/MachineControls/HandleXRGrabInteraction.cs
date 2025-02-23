using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandleXRGrabInteraction : XRGrabInteractable
{
    public ButtonLightController buttonLightController;
    public string buttonName; // The name of the button light to control
    private bool isGrabbed = false;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (buttonLightController != null)
        {
            buttonLightController.TurnButtonOn(buttonName);
            isGrabbed = true;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (buttonLightController != null)
        {
            buttonLightController.TurnButtonOff(buttonName);
            isGrabbed = false;
        }
    }


    public bool isHandleGrabbed()
    {
        return isGrabbed;
    }
}
