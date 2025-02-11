using UnityEngine;

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
            rb.velocity = Vector3.zero; // Stop any movement
            rb.angularVelocity = Vector3.zero; // Stop any rotation
            rb.isKinematic = true; // Optional: Prevents physics interactions
        }

        // Set position using custom snap position values
        obj.position = snapPosition;

        // Set rotation using custom snap rotation values
        obj.rotation = Quaternion.Euler(snapRotation);

        // Optional: Make it a child of the SnapPoint
        obj.SetParent(transform);
    }
}
