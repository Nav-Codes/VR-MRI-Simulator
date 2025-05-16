using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Simulates a sagging cable between two points. Optionally populates with visible segment prefabs,
/// or uses invisible transforms. The cable follows a curved LineRenderer and avoids colliders using raycasts.
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class PhysicsCable : MonoBehaviour
{
    public Transform grabObject;
    public Transform largeObject;
    public int numSegments = 10;
    public float sagAmount = 0.15f;

    [Tooltip("Optional prefab for visual cable segments. Leave null to use invisible transforms.")]
    public GameObject cableSegmentPrefab;

    public float raycastOffset = 0.5f;
    public float segmentHeightOffset = 0.01f;
    public float smoothSpeed = 10f;

    [Tooltip("Which layers should block the cable when raycasting.")]
    public LayerMask collisionLayers = ~0; // Default to Everything

    private List<Transform> segmentTransforms = new List<Transform>();
    private LineRenderer lineRenderer;
    private Transform cableParent;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is missing!");
            return;
        }

        // Create or find the "Cable" parent GameObject under this object
        GameObject cableParentGO = transform.Find("Cable")?.gameObject;
        if (cableParentGO == null)
        {
            cableParentGO = new GameObject("Cable");
            cableParentGO.transform.SetParent(transform);
            cableParentGO.transform.localPosition = Vector3.zero;
            cableParentGO.transform.localRotation = Quaternion.identity;
            cableParentGO.transform.localScale = Vector3.one;
        }
        cableParent = cableParentGO.transform;

        lineRenderer.positionCount = numSegments + 2;

        for (int i = 0; i < numSegments; i++)
        {
            GameObject segmentObj;

            if (cableSegmentPrefab != null)
            {
                segmentObj = Instantiate(cableSegmentPrefab, cableParent);
                var rb = segmentObj.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;
            }
            else
            {
                segmentObj = new GameObject("CableSegment_" + i);
                segmentObj.transform.SetParent(cableParent);
            }

            segmentTransforms.Add(segmentObj.transform);
        }
    }


    void Update()
    {
        Vector3 start = grabObject.position;
        Vector3 end = largeObject.position;

        lineRenderer.SetPosition(0, start);

        for (int i = 0; i < numSegments; i++)
        {
            float t = (i + 1) / (float)(numSegments + 1);
            Vector3 position = Vector3.Lerp(start, end, t);
            position.y -= Mathf.Sin(t * Mathf.PI) * sagAmount;

            // Raycast down from above
            Vector3 rayStart = position + Vector3.up * raycastOffset;
            if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, raycastOffset + 1f, collisionLayers))
            {
                float targetY = hit.point.y + segmentHeightOffset;
                if (targetY > position.y)
                    position.y = targetY;
            }

            Transform segment = segmentTransforms[i];
            Vector3 targetPos = Vector3.Lerp(segment.position, position, Time.deltaTime * smoothSpeed);
            segment.position = targetPos;

            // Optional look direction (if visible prefab)
            if (i > 0 && cableSegmentPrefab != null)
            {
                Vector3 dir = targetPos - segmentTransforms[i - 1].position;
                if (dir != Vector3.zero)
                    segmentTransforms[i - 1].rotation = Quaternion.LookRotation(dir);
            }

            lineRenderer.SetPosition(i + 1, segment.position);
        }

        lineRenderer.SetPosition(numSegments + 1, end);
    }
}
