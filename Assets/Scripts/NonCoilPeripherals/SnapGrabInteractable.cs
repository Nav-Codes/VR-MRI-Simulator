using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[DefaultExecutionOrder(100)]
/// <summary>
/// A custom XRGrabInteractable that allows objects to be grabbed only 
/// if the user's controller is within a specified grab radius.
/// This script also maintains positional and rotational offsets 
/// while ensuring certain axes remain frozen if needed.
/// </summary>
public class SnapGrabInteractable : XRGrabInteractable
{
    [Header("Snap Settings")]
    public Vector3 positionOffset = new Vector3(0, 0, 0.5f);

    [Header("Grab Settings")]
    public float grabRadius = 1.25f; // The radius within which the controller must be to grab

    private Rigidbody rb;
    private Transform controllerTransform;
    private Collider[] colliders;
    private RigidbodyConstraints originalConstraints;
    private bool freezeRotationX, freezeRotationY, freezeRotationZ;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>(true);

        attachEaseInTime = 0f;
        trackPosition = false;
        trackRotation = false;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        if (interactor.transform != null)
        {
            float distance = Vector3.Distance(interactor.transform.position, transform.position);
            return distance <= grabRadius && base.IsSelectableBy(interactor);
        }
        return false;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        controllerTransform = args.interactorObject.transform;

        if (rb != null)
        {
            originalConstraints = rb.constraints;
            freezeRotationX = (rb.constraints & RigidbodyConstraints.FreezeRotationX) != 0;
            freezeRotationY = (rb.constraints & RigidbodyConstraints.FreezeRotationY) != 0;
            freezeRotationZ = (rb.constraints & RigidbodyConstraints.FreezeRotationZ) != 0;

            rb.isKinematic = true; // Disable physics during grab
        }

        foreach (var col in colliders) col.enabled = false;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.constraints = originalConstraints; // Restore original constraints
        }

        foreach (var col in colliders) col.enabled = true;
        controllerTransform = null;
    }

    private void LateUpdate()
    {
        if (controllerTransform == null) return;

        Vector3 targetPosition = controllerTransform.position + controllerTransform.TransformDirection(positionOffset);
        transform.position = targetPosition;

        if (rb == null) return;

        Quaternion targetRotation = controllerTransform.rotation;
        Vector3 euler = targetRotation.eulerAngles;

        if (freezeRotationX) euler.x = transform.rotation.eulerAngles.x;
        if (freezeRotationY) euler.y = transform.rotation.eulerAngles.y;
        if (freezeRotationZ) euler.z = transform.rotation.eulerAngles.z;

        transform.rotation = Quaternion.Euler(euler);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, grabRadius);
    }
}
