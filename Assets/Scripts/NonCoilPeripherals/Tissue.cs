using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tissue : MonoBehaviour
{
    public GameObject TissueObj;
    public GameObject StartScanManager;

    void Update()
    {
        TissueObj.GetComponent<BoxCollider>().enabled = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coil"))
        {
            StartScanManager.GetComponent<StartScanManager>().RevertSmudge(collision.gameObject);
        }
    }
}
