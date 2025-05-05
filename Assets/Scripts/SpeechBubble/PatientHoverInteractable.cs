using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class PatientHoverInteractable : XRBaseInteractable
{
    public GameObject speechBubble;
    public float delayBeforeFade = 1f;
    public float fadeDuration = 3f;

    private int hoverCount = 0;
    public CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        hoverCount++;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if (speechBubble != null)
        {
            canvasGroup = speechBubble.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        hoverCount = Mathf.Max(hoverCount - 1, 0);

        if (hoverCount == 0 && speechBubble != null)
        {
            fadeCoroutine = StartCoroutine(DelayedFadeOut());
        }
    }

    private IEnumerator DelayedFadeOut()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        float time = 0f;
        float startAlpha = canvasGroup.alpha;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
