using UnityEngine.EventSystems;
using UnityEngine;

public class DialButton : MonoBehaviour, IPointerClickHandler
{
    // This variable is used to prevent the button from being pressed multiple times
    private bool canBePressed = true;
    public BedController bedController;

    public void OnPointerClick(PointerEventData eventData)
    {
        canBePressed = bedController.IsAtMinX();
        if (canBePressed)
        {
            canBePressed = true;
            bedController.MoveFixedDistance();
        }
    }
}
