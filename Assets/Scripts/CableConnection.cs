using UnityEngine;

public class CableConnection : MonoBehaviour
{
    public Transform grabObject; // Smaller object to be grabbed
    public Transform largeObject; // Larger object
    public int numSegments = 10;
    public float sagAmount = 0.5f;
    private LineRenderer lineRenderer;

    void Start()
    {
        // Get the LineRenderer component attached to this GameObject
        lineRenderer = GetComponent<LineRenderer>();
        
        // Ensure the LineRenderer component exists
        if (lineRenderer == null)
        {
            Debug.LogError("No LineRenderer component found on the GameObject.");
            return;
        }

        lineRenderer.positionCount = numSegments + 1;
    }

    void Update()
    {
        // Ensure we have a valid LineRenderer
        if (lineRenderer == null) return;

        Vector3 grabPos = grabObject.position;
        Vector3 largePos = largeObject.position;

        for (int i = 0; i <= numSegments; i++)
        {
            float t = i / (float)numSegments;
            // Lerp between grab and large object positions
            Vector3 position = Vector3.Lerp(grabPos, largePos, t);
            // Apply the sag effect using a sine function
            position.y -= Mathf.Sin(t * Mathf.PI) * sagAmount;
            lineRenderer.SetPosition(i, position);
        }
    }
}
