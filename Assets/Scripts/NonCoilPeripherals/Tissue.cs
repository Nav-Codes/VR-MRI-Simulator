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
        Debug.Log("COLLISION DETECTED!!!");
        if (collision.gameObject.CompareTag("Tissue"))
        {
            Debug.Log("TISSUE DETECTED!!!");
        }
        if (collision.gameObject.CompareTag("Coil"))
        {
            Debug.Log("COIL DETECTED!!!");
            StartScanManager.GetComponent<StartScanManager>().RevertSmudge(collision.gameObject);
        }
    }
}
