using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// This class handles the XR interaction for a grabable object, specifically for a handle.
/// It manages the position and rotation of the handle when grabbed, as well as turning a the handle button light on/off.
/// The handle can be attached to a custom parent for specific transformations during interaction.
/// </summary>
public class HandleXRGrabInteraction : XRGrabInteractable
{
    public ButtonLightController buttonLightController;
    public string buttonName;
    public Transform customParent;
    private bool isGrabbed = false;
    private Vector3 localPosition;
    private Quaternion localRotation;
    /// <summary>
    /// Called when the handle is selected (grabbed) by an XR controller.
    /// It turns on the button light and stores the local position and rotation for later use.
    /// </summary>
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (buttonLightController != null)
        {
            buttonLightController.TurnButtonOn(buttonName);
        }

        isGrabbed = true;

        Transform parentToUse = customParent;

        if (parentToUse != null)
        {
            localPosition = parentToUse.InverseTransformPoint(transform.position);
            localRotation = Quaternion.Inverse(parentToUse.rotation) * transform.rotation;
        }
    }
    /// <summary>
    /// Called when the handle is deselected (released) by an XR controller.
    /// It turns off the button light and reattaches the object to the original or custom parent.
    /// It also applies the stored local position and rotation to avoid snapping back.
    /// </summary>
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (buttonLightController != null)
        {
            buttonLightController.TurnButtonOff(buttonName);
        }

        isGrabbed = false;

        // Reattach the object to the custom parent or the original parent if not set
        if (customParent != null)
        {
            transform.SetParent(customParent, true);
        }
        else if (transform.parent != null)
        {
            transform.SetParent(transform.parent, true);
        }

        // Apply the stored local position and rotation to avoid snapping back
        transform.localPosition = localPosition;
        transform.localRotation = localRotation;
    }
    /// <summary>
    /// Continuously updates the position and rotation of the handle when it is being grabbed.
    /// It ensures the handle moves according to the custom or original parent’s transformation.
    /// </summary>
    private void Update()
    {
        if (isGrabbed)
        {
            // Use the custom parent or original parent (fallback if customParent isn't set)
            Transform parentToUse = customParent ? customParent : transform.parent;

            if (parentToUse != null)
            {
                // Update the position and rotation continuously
                transform.position = parentToUse.TransformPoint(localPosition);
                transform.rotation = parentToUse.rotation * localRotation;
            }
        }
    }

    public bool isHandleGrabbed()
    {
        return isGrabbed;
    }
}
