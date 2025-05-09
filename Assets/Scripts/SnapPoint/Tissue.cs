using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tissue : MonoBehaviour, ReturnedInterface
{
    public GameObject TissueObj;
    private List<GameObject> dirtyCoils;

    void Update()
    {
        TissueObj.GetComponent<BoxCollider>().enabled = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coil"))
        {
            RevertSmudge(collision.gameObject);
        }
    }

    public void RevertSmudge(GameObject Coil)
    {
        foreach (Transform child in Coil.transform)
        {
            if (child.gameObject.name.ToLower().Contains("smudge"))
            {
                child.gameObject.SetActive(false);
                dirtyCoils.Remove(Coil);
            }
        }
    }

    public void AddDirtyCoil(GameObject Coil)
    {
        dirtyCoils.Add(Coil);
    }

    public bool isReturned()
    {
        return dirtyCoils.Count == 0;
    }

    public string getReturnedLabel()
    {
        return "Coil Cleaning";
    }
}
