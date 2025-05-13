using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CoilContainerFlipper : MonoBehaviour
{
    public DataBanker dataBanker;
    private bool coilsFlipped = false;
    private List<SnapPointRecord> snapPoints = new();

    void Start()
    {
        foreach (Transform snapPoint in transform)
        {
            if (snapPoint.name != "Head")
            {
                SnapPointRecord record = new SnapPointRecord();
                record.snapPoint = snapPoint.gameObject;
                snapPoints.Add(record);
            }
            
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
        foreach (SnapPointRecord record in snapPoints)
        {
            if (!HasAttachedCoil(record.snapPoint) && !record.isFlipped)
            {
                record.snapPoint.transform.RotateAround(transform.position, Vector3.up, 180);
                record.isFlipped = true;
            }
        }
    }

    public void Unflip()
    {
        foreach (SnapPointRecord record in snapPoints)
        {
            if (!HasAttachedCoil(record.snapPoint) && record.isFlipped)
            {
                record.snapPoint.transform.RotateAround(transform.position, Vector3.up, 180);
                record.isFlipped = false;
            }
        }
    }

    public void UpdateCoilFlip()
    {
        if (coilsFlipped) Flip();
        else Unflip();
    }
}

class SnapPointRecord
{
    public GameObject snapPoint;
    public bool isFlipped = false;
}