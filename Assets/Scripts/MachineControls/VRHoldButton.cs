using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Handles VR button interactions that require holding down the button.
/// Implements <see cref="IPointerDownHandler"/> and <see cref="IPointerUpHandler"/> to detect pointer events.
/// When the button is held, it continuously triggers an action, and when released, it stops.
/// Manages button light states via a <see cref="ButtonLightBase"/> controller.
/// </summary>
public class VRHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public BedController bedController;
    public ButtonLightBase buttonLightController;
    public string type;
    protected bool isHeld = false;
    protected void Update()
    {
        if (isHeld)
        {
            ifHeld();
        }
        else
        {
            ifNotHeld();
        }
    }

    protected virtual void ifHeld()
    {
        LightOn();
    }

    protected virtual void ifNotHeld()
    {
        LightOff();
    }

    protected void LightOn()
    {
        if (buttonLightController == null)
        {
            return;
        }
        buttonLightController.TurnButtonOn(type);
    }

    protected void LightOff()
    {
        if (buttonLightController == null)
        {
            return;
        }
        buttonLightController.TurnButtonOff(type);
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
