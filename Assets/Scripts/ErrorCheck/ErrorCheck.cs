using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ErrorCheck : MonoBehaviour
{
    public List<ErrorGroupEntry> errorGroupEntries = new List<ErrorGroupEntry>(); // Visible in Inspector
    private Dictionary<string, GameObject[]> errorGroups = new Dictionary<string, GameObject[]>(); // Used at runtime

    public GameObject ErrorTextPrefab;    // Prefab with TMP for error message
    public Transform ErrorPanel;          // Panel to display error messages
    public GameObject ContinueButton;     // Button to continue the next steps of the simulation with the errors
    private UnityEngine.Events.UnityAction ContinueClickAction;    // Unity delegate for continue
    public GameObject GoBackButton;       // Button to allow user to check their mistakes before moving on
    private UnityEngine.Events.UnityAction GoBackClickAction;      // Unity delegate for go back
    public DataBanker dataBanker;

    public bool Check(UnityEngine.Events.UnityAction ContinueClick, UnityEngine.Events.UnityAction GoBackClick)
    {
        // Build the dictionary from the serialized list
        errorGroups.Clear();
        foreach (var entry in errorGroupEntries)
        {
            if (!errorGroups.ContainsKey(entry.key))
                errorGroups.Add(entry.key, entry.objects);
        }

        string type = dataBanker.GetExamType();
        GameObject[] Errors = null;
        if (errorGroups != null && errorGroups.ContainsKey(type))
        {
            Errors = errorGroups[type];
        }
        else
        {
            Debug.LogError($"No errors found for type: {type}");
            return false;
        }

        bool all_Correct = true;
        ContinueClickAction = ContinueClick;
        GoBackClickAction = GoBackClick;

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
            CheckerInterface checker = obj.GetComponent<CheckerInterface>();

            if (checker == null)
            {
                Debug.LogError($"{obj.name} does not have a script implementing CheckerInterface!");
                continue;
            }

            bool isCorrect = checker.isCorrect();

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
        ShowButtons(all_Correct, errorText);

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

            if (ColorUtility.TryParseHtmlString("#69EA70", out Color newColor))
                ContinueButton.GetComponent<Image>().color = newColor;
            else
                Debug.LogWarning("Invalid color format!");
        }
    }

    private GameObject AddText(string text, Color color, bool isTitle = false)
    {
        GameObject errorTextObj = Instantiate(ErrorTextPrefab, ErrorPanel);
        LayoutRebuilder.ForceRebuildLayoutImmediate(ErrorPanel.GetComponent<RectTransform>());

        TMP_Text errorText = errorTextObj.GetComponent<TMP_Text>();
        if (errorText == null)
        {
            throw new Exception("ErrorTextPrefab missing TMP_Text component");
        }

        errorText.text = isTitle ? $"<style=\"Title\">{text}</style>" : text;
        errorText.color = color;

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
