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

    //Change this to check if the object has a smudge thing
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
                DirtyObjects.Remove(Coil);
            }
        }
    }

    //could move the apply smudge here since we will have access to all the object that will be dirty 
    //will need the tissue obj in the start scan manager 
    //coils need to by dynamically added to the list
    //could also add them to dirty coils list after the zone 3 screen is pressed (look at what is attached to the bed and add it)
        //need to ensure the list only contains unique items
    //maybe add a box collider to the smudge object itself and when tissue collides with game object with name smudge, disable it

    public void AddDirtyCoil(GameObject Coil)
    {
        DirtyObjects.Add(Coil);
    }

    public void ApplySmudge()
    {
        foreach (GameObject obj in DirtyObjects)
        {
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
        return "Coil Cleaning";
    }
}
