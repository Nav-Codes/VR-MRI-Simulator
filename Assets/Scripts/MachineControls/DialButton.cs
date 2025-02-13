using UnityEngine.EventSystems;
using UnityEngine;

public class DialButton : MonoBehaviour, IPointerClickHandler
{
    public BedController bedController;

    public void OnPointerClick(PointerEventData eventData)
    {
        bedController.MoveFixedDistance();
    }
}
