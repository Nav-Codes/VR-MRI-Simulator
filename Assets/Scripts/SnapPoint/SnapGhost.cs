using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapGhost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coil"))
        {
            if (gameObject.GetComponent<MeshFilter>().sharedMesh == other.gameObject.GetComponentInParent<MeshFilter>().sharedMesh)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
