using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public GameObject userSpeechPanel;
    public GameObject patientSpeechPanel;
    public ShowSpeechButtonZone showSpeechButtonZone;

    private TMPro.TextMeshProUGUI userText;
    private TMPro.TextMeshProUGUI patientText;
    private string dialogueType = "intro";
    private string userSpeechTemplate;

    // Start is called before the first frame update
    void Start()
    {
        userText = userSpeechPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (userText == null)
        {
            Debug.LogError("userText object missing");
        }
        userText.text = "Which patient am I scanning next?";
        SetUserSpeechVisibility(true);

        patientText = patientSpeechPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (patientText == null)
        {
            Debug.LogError("patientText object missing");
        }
        SetPatientSpeechVisibility(false);

        //string msg = @"Hello {0}";
        //msg = string.Format(msg, "...");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUserText(string text)
    {
        userText.text = text;
    }

    public void SetUserSpeechVisibility(bool isVisible)
    {
        userSpeechPanel.SetActive(isVisible);
    }

    public void SetUserSpeechTemplate(string template)
    {
        userSpeechTemplate = template;
    }

    public void SetPatientText(string text)
    {
        patientText.text = text;
    }

    public void SetPatientSpeechVisibility(bool isVisible)
    {
        patientSpeechPanel.SetActive(isVisible);
    }

    public void InitiateDialogue(string userDialogue, string patientDialogue, float duration)
    {
        SetUserText(userDialogue);
        SetUserSpeechVisibility(true);
        SetPatientText(patientDialogue);
        SetPatientSpeechVisibility(true);
    }
}
