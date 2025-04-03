using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantizieHands : MonoBehaviour , CheckerInterface
{
    private DataBanker dataBanker;

    private bool CORRECT = false;

    void Start()
    {
        dataBanker = FindObjectOfType<DataBanker>();

        if (dataBanker == null)
        {
            Debug.LogError("DataBanker not found in the scene!");
        }

    }

    public void OnMouseDown()
    {
        Debug.Log("Clicked on: " + gameObject.name);

        if (gameObject.name.Contains("Soap"))
        {
            CORRECT = true;
            Debug.Log("Soap interaction registered.");
        }
        else if (gameObject.name.Contains("Sanitizer"))
        {
            CORRECT = true;
            Debug.Log("Sanitizer interaction registered.");
        }
        else
        {
            Debug.Log("Object is not a Soap or Sanitizer.");
        }
    }

    public bool isCorrect()
    {
        if (CORRECT)
        {
            return CORRECT;
        }
        else
        {
            return false;
        }
    }

    public string getLabel()
    {
        return "Hands Santized";
    }
}
