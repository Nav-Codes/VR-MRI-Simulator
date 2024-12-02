using UnityEngine;
using UnityEngine.EventSystems;

public class VRHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TableController tableController;
    public TrayController trayController;
    public bool moveUp; // True for Button_Up, False for Button_Down

    private bool isHeld = false;

    void Update()
    {
        if (isHeld)
        {
            if (moveUp)
            {
                // Move up logic
                if (!tableController.IsAtMax())
                {
                    tableController.MoveUp();
                }
                else if (!trayController.IsAtMax())
                {
                    trayController.MoveUp();
                }
            }
            else
            {
                // Move down logic
                if (!trayController.IsAtMin())
                {
                    trayController.MoveDown();
                }
                else if (!tableController.IsAtMin())
                {
                    tableController.MoveDown();
                }
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
