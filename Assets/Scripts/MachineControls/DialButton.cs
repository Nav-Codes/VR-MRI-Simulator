using UnityEngine.EventSystems;
using UnityEngine;
/// <summary>
/// This class handles the interaction with a dial button, which triggers actions based on pointer events.
/// It implements both the <see cref="IPointerDownHandler"/> and <see cref="IPointerUpHandler"/> interfaces to detect when the button is pressed or released.
/// When pressed, it checks if the bed is at its minimum position and moves the bed a fixed distance if possible. 
/// It also tracks whether the button is being held down and updates the dial's position accordingly, using the <see cref="DialController"/>.
/// </summary>
public class DialButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // This variable is used to prevent the button from being pressed multiple times
    private bool canBePressed = true;
    public BedController bedController;
    public GameObject dial;
    private bool isHolding = false; // Flag to indicate when the button is being held

    public void OnPointerDown(PointerEventData eventData)
    {
        canBePressed = bedController.IsAtMinX();
        if (canBePressed)
        {
            canBePressed = true;
            bedController.MoveFixedDistance();
        }
        isHolding = true; // Start tracking hold
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false; // Stop tracking hold
    }

    private void Update()
    {
        if (isHolding)
        {

            dial.GetComponent<DialController>().DialDown();
        } else
        {
            dial.GetComponent<DialController>().DialUp();
        }
    }
}