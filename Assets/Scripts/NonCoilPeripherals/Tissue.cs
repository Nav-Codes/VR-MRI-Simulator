using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tissue : MonoBehaviour, CheckerInterface
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

    public bool isCorrect()
    {
        //maybe on start scan, we can assign the coil object that is being used somewhere, 
        //then access that coil object to check if the smudge object is active 

        //if we can assign a tissue object thingy in the start scan manager, mmay not need start scan manager game object in here
        return dirtyCoils.Count == 0;
    }

    public string getLabel()
    {
        return "Coil Cleaning";
    }
}
