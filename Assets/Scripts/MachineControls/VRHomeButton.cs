using UnityEngine;
using UnityEngine.EventSystems;

public class VRHomeButton : MonoBehaviour, IPointerClickHandler
{
    public BedController bedController;

    public void OnPointerClick(PointerEventData eventData)
    {
        bedController.HomePosition();
    }
}
