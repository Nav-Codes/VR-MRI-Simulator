using UnityEngine.EventSystems;
using UnityEngine;

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