using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// The VRHoldButton class manages a button's behavior in a VR environment, specifically for controlling the movement of a bed object.
/// When the button is held down, it triggers either the MoveUp or MoveDown action in the BedController based on the moveUp boolean.
/// The class also controls the lighting of the button, turning it on or off using the ButtonLightController based on whether the button is being held.
/// This interaction is handled through IPointerDownHandler and IPointerUpHandler, which detect when the button is pressed or released.
/// </summary>
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
