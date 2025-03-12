using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableCanvas : MonoBehaviour, IPointerClickHandler
{
    public GameObject uiPanel;
    public GameObject[] closeOnOpen;
    public GameObject[] closeOnClose;
    private static bool isOpen = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        uiPanel.transform.localScale = isOpen ?new Vector3(1, 1, 1) : new Vector3(5, 5, 1);
        setActive(closeOnOpen, isOpen);
        isOpen = !isOpen;
        setActive(closeOnClose, isOpen);
    }
    private void setActive(GameObject[] objects, bool active)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(active);
        }
    }
}
