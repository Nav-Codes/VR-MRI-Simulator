using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DialRotation : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Quaternion initialGrabRotation;
    private Quaternion initialControllerRotation;

    public float dialXRotation = -90.0f; // Lock X rotation to 0 degrees
    public float dialYRotation = 0.0f;  // Lock Y rotation to 0 degrees
    public float dialZRotation = 90.0f; // Lock Z rotation to 90 degrees
    public float rotationSpeed = 100.0f; // Speed of easing rotation when released

    public float minRotationX = -120.0f; // Min limit for X rotation
    public float maxRotationX = -60.0f;  // Max limit for X rotation

    public BedController bedController; // Reference to the bed controller

    public float minimumAngleDifference = 3.0f; // Minimum angle difference to trigger movement

    private Quaternion targetRotation; // The target rotation when released
    private bool isReleased = false; // Flag to indicate when the object has been released

    void Start()
    {
        // Get XRGrabInteractable from the parent object (pivot)
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        // Disable position and rotation tracking
        grabInteractable.trackPosition = false;
        grabInteractable.trackRotation = false; // Disable rotation tracking
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Save the initial rotation of the parent (pivot) and controller
        initialGrabRotation = transform.rotation;
        initialControllerRotation = args.interactorObject.transform.rotation;
    }

    private void Update()
    {
        if (grabInteractable.isSelected)
        {
            // Check if there are any interactors selecting the object
            if (grabInteractable.interactorsSelecting.Count > 0)
            {
                Transform interactor = grabInteractable.interactorsSelecting[0].transform;

                // Calculate the rotation difference between the initial and current controller rotation
                Quaternion rotationDifference = interactor.rotation * Quaternion.Inverse(initialControllerRotation);
                Vector3 eulerRotation = rotationDifference.eulerAngles;

                // Apply the rotation only on the X-axis
                float newXRotation = initialGrabRotation.eulerAngles.x + eulerRotation.x;

                // Normalize the angle before clamping
                newXRotation = NormalizeAngle(newXRotation);

                // Clamp the new X rotation to be within the min and max values
                newXRotation = Mathf.Clamp(newXRotation, minRotationX, maxRotationX);

                // Lock Y and Z rotations to their initial values, but apply new X rotation
                transform.rotation = Quaternion.Euler(
                    newXRotation,  // Apply X-axis rotation
                    dialYRotation, // Lock Y-axis
                    dialZRotation  // Lock Z-axis
                );
                if ((newXRotation - dialXRotation) > minimumAngleDifference)
                {
                    bedController.MoveDown();

                }
                else if ((dialXRotation - newXRotation) > minimumAngleDifference)
                {
                    bedController.MoveUp();

                }
            }
            else
            {
                Debug.LogWarning("No interactors found in interactorsSelecting.");
            }
        }
        else if (!grabInteractable.isSelected && isReleased)
        {
            // Smoothly rotate the dial back to the target rotation
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    // Function to normalize the angle within -180 to 180 range
    float NormalizeAngle(float angle)
    {
        // Normalize to [0, 360)
        angle = Mathf.Repeat(angle, 360f);
        // Convert to [-180, 180]
        if (angle > 180f) angle -= 360f;
        return angle;
    }


    private void OnRelease(SelectExitEventArgs args)
    {
        // Set the target rotation to the locked final values when released
        targetRotation = Quaternion.Euler(dialXRotation, dialYRotation, dialZRotation);

        // Indicate that the object has been released
        isReleased = true;
    }
}
