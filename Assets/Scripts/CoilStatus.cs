using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilStatus : MonoBehaviour, CheckerInterface
{
    private List<GameObject> coils = new List<GameObject>(); // Use List instead of Array
    public DataBanker dataBanker;
    // Start is called before the first frame update
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
                return (coil.transform.GetChild(0).childCount != 0);
            }
        }
        return false;
    }

    public string getLabel() {
        return "Coil Selection";
    }
}
