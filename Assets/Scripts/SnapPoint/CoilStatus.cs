using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilStatus : MonoBehaviour, CheckerInterface, ReturnedInterface
{
    private List<GameObject> coils = new List<GameObject>();
    public DataBanker dataBanker;
    public Container shelfParent;
    void Start()
    {
        foreach (Transform child in transform)
        {
            coils.Add(child.gameObject);
        }
    }

    public bool isCorrect() {
        foreach (GameObject coil in coils)
        {
            if (dataBanker.GetExamType().Contains(coil.name))
            {
                int numParts = coil.transform.childCount;
                int checkNumParts = 0;

                foreach (Transform childCoil in coil.transform)
                    if (childCoil.childCount > 0) checkNumParts++;
                    
                if (checkNumParts == numParts) return true; 
            }
        }
        return false;
    }

    public bool isReturned() {
        foreach (GameObject coil in coils)
        {
            if (dataBanker.GetExamType().Contains(coil.name))
            {
                int numParts = coil.transform.childCount;
                int checkNumParts = 0;

                foreach (Transform childCoil in coil.transform)
                    if (childCoil.childCount > 0) checkNumParts++;
                    
                if (checkNumParts != 0 || !shelfParent.Contains(coil.transform)) return false ; 
            }
        }
        return true;
    }

    public string getLabel() {
        return "Coil Selection";
    }

    public string getReturnedLabel() {
        return "All Coils";
    }
}
