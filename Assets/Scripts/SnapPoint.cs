using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    public float snapDistance = 0.2f; // Maximum distance for snapping
    public float snapPosX = 0.0f; // Snap X position
    public float snapPosY = 0.0f; // Snap Y position
    public float snapPosZ = 0.0f; // Snap Z position
    public float snapAngleX = 0.0f; // Snap X rotation
    public float snapAngleY = 0.0f; // Snap Y rotation
    public float snapAngleZ = 0.0f; // Snap Z rotation
    public string snappableTag = "Snappable"; // Tag for objects that can snap

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[SnapPoint] Object entered trigger: {other.name}");

        if (other.CompareTag(snappableTag))
        {
            Debug.Log($"[SnapPoint] Object '{other.name}' has the correct tag '{snappableTag}'. Checking distance...");
            TrySnapObject(other.transform);
        }
        else
        {
            Debug.Log($"[SnapPoint] Object '{other.name}' does not have the correct tag '{snappableTag}'. Ignoring.");
        }
    }

    private void TrySnapObject(Transform obj)
    {
        float distance = Vector3.Distance(obj.position, transform.position);
        Debug.Log($"[SnapPoint] Distance from '{obj.name}' to SnapPoint: {distance}");

        if (distance <= snapDistance)
        {
            Debug.Log($"[SnapPoint] Object '{obj.name}' is within snap distance ({snapDistance}). Snapping...");
            SnapObject(obj);
        }
        else
        {
            Debug.Log($"[SnapPoint] Object '{obj.name}' is too far away to snap.");
        }
    }

    private void SnapObject(Transform obj)
    {
        // Set position using custom snap position values
        obj.position = new Vector3(snapPosX, snapPosY, snapPosZ);

        // Set rotation using custom snap rotation values
        obj.rotation = Quaternion.Euler(snapAngleX, snapAngleY, snapAngleZ);

        Debug.Log($"[SnapPoint] Object '{obj.name}' snapped into position at {transform.position}");

        // Disable gravity if the object has a Rigidbody
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero; // Stop any movement
            rb.angularVelocity = Vector3.zero; // Stop any rotation
            rb.isKinematic = true; // Optional: Prevents physics interactions
        }

        // Optional: Make it a child of the SnapPoint
        obj.SetParent(transform);
        Debug.Log($"[SnapPoint] Object '{obj.name}' is now a child of '{transform.name}'.");
    }
}
