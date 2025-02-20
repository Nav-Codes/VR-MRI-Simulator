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
            Move();
            LightOn();
        }
        else
        {
            LightOff();
        }
    }

    private void Move()
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

    private void LightOn()
    {
        if (buttonLightController == null)
        {
            return;
        }
        if (moveUp)
        {

            buttonLightController.TurnButtonOn("Up");

        }
        else
        {

            buttonLightController.TurnButtonOn("Down");

        }
    }

    private void LightOff()
    {
        if (buttonLightController == null)
        {
            return;
        }
        if (moveUp)
        {

            buttonLightController.TurnButtonOff("Up");

        }
        else
        {

            buttonLightController.TurnButtonOff("Down");

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
