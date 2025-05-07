using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// The SnapPoint class allows a specific object to snap to a defined position and rotation when it enters a trigger area.
/// Instead of checking tags, it directly compares the colliding GameObject with the expected reference.
/// </summary>
public class SnapPoint : MonoBehaviour
{
    public float snapDistance = 0.2f; // Maximum distance for snapping
    public Vector3 snapPosition; // Position to snap to
    public Vector3 snapRotation; // Rotation to snap to
    public GameObject expectedObject; // The specific object that can snap

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == expectedObject) // Compare by reference
        {
            TrySnapObject(other.transform);
        }
    }

    private void TrySnapObject(Transform obj)
    {
        Debug.Log("Trying to snap object: " + obj.name);
        float distance = Vector3.Distance(obj.position, transform.position);

        if (distance <= snapDistance)
        {
            Debug.Log("Snapping object: " + obj.name);
            SnapObject(obj);
        }
    }

    private void SnapObject(Transform obj)
    {
        // Disable gravity if the object has a Rigidbody
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Attempt to set the parent
        obj.SetParent(transform);

        obj.gameObject.layer = LayerMask.NameToLayer("SnappedObjects");

        // Set position using custom snap position values
        obj.localPosition = snapPosition;

        // Set rotation using custom snap rotation values
        obj.localRotation = Quaternion.Euler(snapRotation);

        XRGrabInteractable grab = obj.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            // Make sure "SnappedObjects" is defined in Project Settings > XR Interaction Toolkit
            grab.interactionLayers = InteractionLayerMask.GetMask("SnappedObjects");
        }

        // Check if the object implements ISnappable and call OnSnapped()
        ISnappable snappable = obj.GetComponent<ISnappable>();
        if (snappable != null)
        {
            snappable.OnSnapped(transform.parent);
        }
    }
}

// Interface for custom snap behavior
public interface ISnappable
{
    void OnSnapped(Transform snapPointParent);
}
