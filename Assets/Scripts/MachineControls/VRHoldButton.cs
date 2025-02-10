using UnityEngine;
using UnityEngine.EventSystems;

public class VRHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public BedController bedController;
    public bool moveUp; // True for Button_Up, False for Button_Down

    private bool isHeld = false;

    void Update()
    {
        if (isHeld)
        {
            if (moveUp)
            {
                bedController.MoveUp();
            }
            else
            {
                bedController.MoveDown();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
    }
}
