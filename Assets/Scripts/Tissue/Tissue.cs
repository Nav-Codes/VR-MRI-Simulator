using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tissue : MonoBehaviour, ReturnedInterface
{
    public GameObject TissueObj;
    public List<GameObject> DirtyObjects = new List<GameObject>();

    void Update()
    {
        TissueObj.GetComponent<BoxCollider>().enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.ToLower().Contains("smudge"))
        {
            RevertSmudge(other.gameObject);
        }
    }

    public void RevertSmudge(GameObject smudge)
    {
        smudge.SetActive(false);
        DirtyObjects.Remove(smudge.transform.parent.gameObject);
    }

    public void AddDirtyObject(GameObject Coil)
    {
        DirtyObjects.Add(Coil);
    }

    public void ApplySmudge()
    {
        foreach (GameObject obj in DirtyObjects)
        {
            Debug.Log("DIRTY OBJECT: [" + obj + "]");
            foreach (Transform smudge in obj.transform)
            {
                if (smudge.gameObject.name.ToLower().Contains("smudge"))
                {
                    smudge.gameObject.SetActive(true);
                }
            }
        }
    }

    //make this check if all the smudge game objects are disabled
    public bool isReturned()
    {
        return DirtyObjects.Count == 0;
    }

    public string getReturnedLabel()
    {
        return "Equipment Cleaning";
    }
}
