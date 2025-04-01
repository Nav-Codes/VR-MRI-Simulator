using UnityEngine;
/// <summary>
/// The SnapPoint class allows objects with a specific tag to snap to a defined position and rotation when they enter a trigger area.
/// When an object with the appropriate tag (e.g., "Snappable") enters the snap point's trigger zone, it will be snapped to the defined position and rotation.
/// The object will also have its Rigidbody physics disabled (if present) to prevent unintended movement.
/// Additionally, if the object implements the ISnappable interface, it will call the OnSnapped method to notify it of the snap action.
/// </summary>
public class SnapPoint : MonoBehaviour
{
    public float snapDistance = 0.2f; // Maximum distance for snapping
    public Vector3 snapPosition; // Position to snap to
    public Vector3 snapRotation; // Rotation to snap to
    public string snappableTag = "Snappable"; // Tag for objects that can snap

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(snappableTag))
        {
            TrySnapObject(other.transform);
        }
    }

    private void TrySnapObject(Transform obj)
    {
        float distance = Vector3.Distance(obj.position, transform.position);

        if (distance <= snapDistance)
        {
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

        // Set position using custom snap position values
        obj.localPosition = snapPosition;

        // Set rotation using custom snap rotation values
        obj.rotation = Quaternion.Euler(snapRotation);

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
