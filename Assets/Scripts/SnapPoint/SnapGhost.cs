using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapGhost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coil"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            //could compare mesh renderers to see if they are the exact same to ensure that only that coils ghost spawns
        }
    }
}
