using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorCheck : MonoBehaviour
{
    public GameObject[] Errors;           // Array of objects implementing CheckerInterface
    public GameObject ErrorTextPrefab;    // Prefab with TMP for error message
    public Transform ErrorPanel;          // Panel to display error messages

    void Start()
    {
        Debug.Log("ErrorCheck script initialized.");
    }

    public void Check()
    {
        Debug.Log("Starting error check...");

        if (Errors == null || Errors.Length == 0)
        {
            Debug.LogWarning("No objects in Errors array!");
            return;
        }

        foreach (GameObject obj in Errors)
        {
            Debug.Log($"Checking object: {obj.name}");

            // Check if the GameObject has a component implementing CheckerInterface
            CheckerInterface checker = obj.GetComponent<CheckerInterface>();

            if (checker == null)
            {
                Debug.LogError($"{obj.name} does not have a script implementing CheckerInterface!");
                continue; // Skip to the next object
            }

            bool isCorrect = checker.isCorrect();
            Debug.Log($"{obj.name} check result: {(isCorrect ? "Correct ✅" : "Not Correct ❌")}");

            // Instantiate the error text
            GameObject errorTextObj = Instantiate(ErrorTextPrefab, ErrorPanel);
            Debug.Log($"Instantiated error text for {obj.name}");

            // Get the TextMeshPro component
            TMP_Text errorText = errorTextObj.GetComponent<TMP_Text>();
            if (errorText == null)
            {
                Debug.LogError("ErrorTextPrefab must have a TMP_Text component!");
                return;
            }

            // Update text and color
            errorText.text = checker.getLabel() + (isCorrect ? " is correct" : " is not correct");
            errorText.color = isCorrect ? Color.green : Color.red;

            Debug.Log($"Set text: {errorText.text}, Color: {(isCorrect ? "Green" : "Red")}");
        }

        Debug.Log("Error check complete.");
    }
}
