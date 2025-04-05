using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class ErrorCheck : MonoBehaviour
{
    public GameObject[] Errors;           // Array of objects implementing CheckerInterface
    public GameObject ErrorTextPrefab;    // Prefab with TMP for error message
    public Transform ErrorPanel;          // Panel to display error messages

    void Start()
    {
        Debug.Log("ErrorCheck script initialized.");
    }

    public bool Check()
    {
		bool all_Correct = true;

        Debug.Log("Starting error check...");

        if (Errors == null || Errors.Length == 0)
        {
            Debug.LogWarning("No objects in Errors array!");
            return false;
        }

        ClearText();
        AddText("Procedure Feedback", Color.black, true);

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

            try
            {
                AddText((checker.getLabel() + (isCorrect ? " is correct" : " is not correct")), (isCorrect ? Color.green : Color.red));
            }
            catch (Exception e) 
            {
                Debug.Log(e.ToString());
                return false;
            }

			all_Correct = all_Correct && isCorrect;
        }

        ErrorPanel.gameObject.SetActive( true );

        Debug.Log("Error check complete.");
		return all_Correct;
    }

    private void AddText(string text, Color color, bool isTitle = false)
    {
        // Instantiate the error text
        GameObject errorTextObj = Instantiate(ErrorTextPrefab, ErrorPanel);
        LayoutRebuilder.ForceRebuildLayoutImmediate(ErrorPanel.GetComponent<RectTransform>());

        // Get the TextMeshPro component
        TMP_Text errorText = errorTextObj.GetComponent<TMP_Text>();
        if (errorText == null)
        {
            throw new Exception("ErrorTextPrefab missing TMP_Text component");
        }

        // Update text and color
        errorText.text = isTitle ? $"<style=\"Title\">{text}</style>" : text;
        errorText.color = color;

        Debug.Log($"Set text: {errorText.text}, Color: {errorText.color.ToString()}");
    }

    private void ClearText()
    {
        foreach (Transform child in ErrorPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
