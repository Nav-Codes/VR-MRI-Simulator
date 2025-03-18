using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ClickableCanvas : MonoBehaviour, IPointerClickHandler
{
    public GameObject uiPanel;
    public GameObject[] closeOnOpen;
    public GameObject[] closeOnClose;
    private static bool isOpen = false;

    private Vector3 expandedScale = new Vector3(5, 5, 1);
    private Vector3 collapsedScale = new Vector3(1, 1, 1);
    private float animationDuration = 0.3f;

    public void OnPointerClick(PointerEventData eventData)
    {
        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(AnimateAndToggle(isOpen));
    }

    private IEnumerator AnimateAndToggle(bool opening)
    {
        yield return StartCoroutine(ScaleOverTime(opening ? expandedScale : collapsedScale));
        setActive(closeOnOpen, !opening);
        setActive(closeOnClose, opening);
    }

    private IEnumerator ScaleOverTime(Vector3 targetScale)
    {
        float elapsedTime = 0f;
        Vector3 startScale = uiPanel.transform.localScale;

        while (elapsedTime < animationDuration)
        {
            uiPanel.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        uiPanel.transform.localScale = targetScale; 
    }

    private void setActive(GameObject[] objects, bool active)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null) obj.SetActive(active);
        }
    }
}
