using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public GameObject userSpeechPanel;
    public SpeechBubbleBuilder patientSpeechBubble;
    public ShowSpeechButtonZone showSpeechButtonZone;

    private TMPro.TextMeshProUGUI userText;
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

        //string msg =
        //    @"Dear {0},

        //    Your job finished at {1} and your file is available for download at {2}.

        //    Regards,

        //    --
        //    {3}";
        //            msg = string.Format(msg, user, finishTime, fileUrl, signature);
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
}
