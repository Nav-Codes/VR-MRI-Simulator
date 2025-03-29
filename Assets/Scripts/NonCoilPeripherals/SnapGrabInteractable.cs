using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[DefaultExecutionOrder(100)]
public class SnapGrabInteractable : XRGrabInteractable
{
    [Header("Snap Settings")]
    public Vector3 positionOffset = new Vector3(0, 0, 0.5f);

    private Rigidbody rb;
    private Transform controllerTransform;
    private Collider[] colliders;
    private RigidbodyConstraints originalConstraints;
    private bool freezeRotationX, freezeRotationY, freezeRotationZ; // Track frozen axes

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>(true);
        
        attachEaseInTime = 0f;
        trackPosition = false;
        trackRotation = false;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        controllerTransform = args.interactorObject.transform;

        if (rb != null)
        {
            // Store original constraints and frozen rotation states
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

        // Calculate target position (always applied)
        Vector3 targetPosition = controllerTransform.position + 
                               controllerTransform.TransformDirection(positionOffset);
        transform.position = targetPosition;

        // Only apply rotation for UNFROZEN axes
        if (rb == null) return;
        
        Quaternion targetRotation = controllerTransform.rotation;
        Vector3 euler = targetRotation.eulerAngles;
        
        // Preserve original rotation for frozen axes
        if (freezeRotationX) euler.x = transform.rotation.eulerAngles.x;
        if (freezeRotationY) euler.y = transform.rotation.eulerAngles.y;
        if (freezeRotationZ) euler.z = transform.rotation.eulerAngles.z;
        
        transform.rotation = Quaternion.Euler(euler);
    }
}