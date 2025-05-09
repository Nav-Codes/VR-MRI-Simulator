using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CoilContainerFlipper : MonoBehaviour
{
    public DataBanker dataBanker;
    public Transform headCoil;
    //private List<GameObject> snapPoints = new List<GameObject>();
    //private List<Transform> originalSnapPointLocations = new();
    private List<SnapPointRecord> snapPoints = new();

    void Start()
    {
        foreach (Transform snapPoint in transform)
        {
            //snapPoints.Add(child.gameObject);
            //originalSnapPointLocations.Add(child.transform);
            SnapPointRecord record = new SnapPointRecord();
            record.snapPoint = snapPoint.gameObject;
            record.originalPosition = snapPoint.transform.position;
            record.originalRotation = snapPoint.transform.rotation;
            snapPoints.Add(record);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool HasAttachedCoil(GameObject snapPoint)
    {
        foreach (Transform childCoil in snapPoint.transform)
            if (childCoil.childCount > 0) return true;

        return false;
    }

    public void Flip()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 180);
        foreach (SnapPointRecord record in snapPoints)
        {
            if (HasAttachedCoil(record.snapPoint) || record.snapPoint.name == "Head")
            {
                Debug.Log(record.snapPoint.transform);
                record.snapPoint.transform.SetPositionAndRotation(
                    record.originalPosition, 
                    record.originalRotation
                );
            }
        }
    }

    public void Unflip()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        foreach (SnapPointRecord record in snapPoints)
        {
            record.snapPoint.transform.SetPositionAndRotation(
                record.originalPosition,
                record.originalRotation
            );
        }
    }
}

class SnapPointRecord
{
    public GameObject snapPoint;
    public Vector3 originalPosition;
    public Quaternion originalRotation;
}