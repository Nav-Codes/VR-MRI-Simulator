using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallBell : MonoBehaviour, ISnappable, CheckerInterface
{
    private Transform originalParent;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isInHand = false;

    // Start is called before the first frame update
    void Start()
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void ResetPosition()
    {
        Debug.Log("Parent: " + originalParent);
        transform.SetParent(originalParent);
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        isInHand = false;
    }

    public bool isCorrect()
    {
        return isInHand;
    }

    public string getLabel()
    {
        return "Call bell";
    }

    public void OnSnapped(Transform snapPointParent)
    {
        isInHand = true;
    }
}