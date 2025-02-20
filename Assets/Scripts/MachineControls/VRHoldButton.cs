using UnityEngine;
using UnityEngine.EventSystems;

public class VRHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public BedController bedController;
    public ButtonLightController buttonLightController = null;
    public bool moveUp; // True for Button_Up, False for Button_Down

    private bool isHeld = false;

    void Update()
    {
        if (isHeld)
        {
            if (moveUp)
            {
                bedController.MoveUp();
                if (buttonLightController != null)
                {
                    buttonLightController.UpButtonTurnOn();
                }
            }
            else
            {
                bedController.MoveDown();
                if (buttonLightController != null)
                {
                    buttonLightController.DownButtonTurnOn();
                }
            }
        } else {
            if (moveUp)
            {
                if (buttonLightController != null)
                {
                    buttonLightController.UpButtonTurnOff();
                }
            }
            else
            {
                if (buttonLightController != null)
                {
                    buttonLightController.DownButtonTurnOff();
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
