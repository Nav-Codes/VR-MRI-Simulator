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
    public GameObject ContinueButton;     // Button to continue the next steps of the simulation with the errors
    private UnityEngine.Events.UnityAction ContinueClickAction;    // Unity delegate that defines what to do when continue button is clicked
    public GameObject GoBackButton;  // Button to allow user to check their mistakes before moving on to the next steps
    private UnityEngine.Events.UnityAction GoBackClickAction; // Unity delegate that defines what to do when check errors button is clicked

    void Start()
    {
        Debug.Log("ErrorCheck script initialized.");
    }

    public bool Check(UnityEngine.Events.UnityAction ContinueClick = null, UnityEngine.Events.UnityAction GoBackClick = null)
    {
        bool all_Correct = true;
        ContinueClickAction = ContinueClick;
        GoBackClickAction = GoBackClick;

        Debug.Log("Starting error check...");

        if (Errors == null || Errors.Length == 0)
        {
            Debug.LogWarning("No objects in Errors array!");
            return false;
        }

        ClearText();
        AddText("Procedure Feedback", Color.black, true);
        GameObject errorText = AddText("Please fix your errors before continuing", Color.black, false);
        errorText.SetActive(false);

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

        ErrorPanel.gameObject.SetActive(true);

        //set active the buttons on the canvas
        ShowButtons(all_Correct, errorText);

        Debug.Log("Error check complete.");

        return all_Correct;
    }

    public void ClickContinue()
    {
        ContinueClickAction();
        DisablePanel();
    }

    public void ClickGoBack()
    {
        GoBackClickAction();
        DisablePanel();
    }

    private void ShowButtons(bool allCorrect, GameObject errorText)
    {
        if (!allCorrect)
        {
            ContinueButton.SetActive(true);
            GoBackButton.SetActive(true);
            errorText.SetActive(true);
        }
        else
        {
            GoBackButton.SetActive(false);
            errorText.SetActive(false);
            ContinueButton.SetActive(true);
            
            Color newColor;
            if (ColorUtility.TryParseHtmlString("#69EA70", out newColor))
                ContinueButton.GetComponent<Image>().color = newColor;
            else
                Debug.LogWarning("Invalid color format!");
        }
    }

    private GameObject AddText(string text, Color color, bool isTitle = false)
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

        return errorTextObj;
    }

    private void ClearText()
    {
        foreach (Transform child in ErrorPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private IEnumerator DisablePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ErrorPanel.gameObject.SetActive(false);
        ContinueButton.SetActive(false);
        GoBackButton.SetActive(false);
    }

    private void DisablePanel()
    {
        ErrorPanel.gameObject.SetActive(false);
        ContinueButton.SetActive(false);
        GoBackButton.SetActive(false);
    }
}
