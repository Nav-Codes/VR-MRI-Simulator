using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class SpeechBubbleBuilder : MonoBehaviour
{
    public Transform headTransform;
    public Vector3 offset = new Vector3(0, 0.2f, 0);
    public string bubbleText = "Hello!";
    public GameObject bubbleInstance;

    void Awake()
    {
        // Create empty holder
        GameObject bubble = new GameObject("SpeechBubble");
        bubble.transform.SetParent(headTransform);
        bubble.transform.localPosition = offset;
        bubble.transform.localRotation = Quaternion.identity;

        // Billboard
        bubble.AddComponent<Billboard>();

        // Canvas setup
        Canvas canvas = CreateCanvas(bubble.transform);
        AddPanelWithText(canvas.transform, bubbleText);

        // Create a CanvasGroup to handle the visibility
        CanvasGroup canvasGroup = bubble.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;  // Start as invisible but keep the interaction
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        bubbleInstance = bubble;

        // Hook to hover script
        var hover = GetComponent<PatientHoverInteractable>();
        if (hover != null) hover.speechBubble = bubble;
    }

    Canvas CreateCanvas(Transform parent)
    {
        GameObject canvasGO = new GameObject("BubbleCanvas");
        canvasGO.transform.SetParent(parent);
        canvasGO.transform.localPosition = Vector3.zero;
        canvasGO.transform.localRotation = Quaternion.identity;
        canvasGO.transform.localScale = Vector3.one * 0.003f;

        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvasGO.AddComponent<CanvasScaler>().dynamicPixelsPerUnit = 10f;

        var trackedDeviceRaycaster = canvasGO.AddComponent<TrackedDeviceGraphicRaycaster>();
        trackedDeviceRaycaster.enabled = true;

        return canvas;
    }

    void AddPanelWithText(Transform canvas, string text)
    {
        // Panel
        GameObject panel = new GameObject("Panel");
        panel.transform.SetParent(canvas);
        panel.transform.localRotation = Quaternion.identity;
        Image image = panel.AddComponent<Image>();
        image.color = new Color(1f, 1f, 1f, 0.8f);

        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(0.5f, 0.2f);
        panelRect.localPosition = Vector3.zero;

        // Text
        GameObject textGO = new GameObject("Text");
        textGO.transform.SetParent(panel.transform);
        textGO.transform.localRotation = Quaternion.identity;
        TMP_Text tmp = textGO.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = 0.1f;
        tmp.color = Color.black;
        tmp.alignment = TextAlignmentOptions.Center;

        RectTransform textRect = textGO.GetComponent<RectTransform>();
        textRect.sizeDelta = panelRect.sizeDelta;
        textRect.localPosition = Vector3.zero;

        // Add a Button component to the panel to make it clickable
        Button btn = panel.AddComponent<Button>();
        btn.onClick.AddListener(() => Debug.Log("Speech bubble clicked."));

        // Ensure raycasting is enabled for the panel and its children
        var imageComp = panel.GetComponent<Image>();
        if (imageComp != null)
        {
            imageComp.raycastTarget = true;
        }

        var textComp = textGO.GetComponent<TMP_Text>();
        if (textComp != null)
        {
            textComp.raycastTarget = true;
        }
    }
}
