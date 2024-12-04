using UnityEngine;
using UnityEngine.EventSystems;

public class MoveTrayUpButton : MonoBehaviour, IPointerClickHandler
{
    public TableController tableController;
    public TrayController trayController;

    public float moveAmount = 11.0f; // The amount to move the tray by

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("MoveTrayUpButton: Button clicked");

        if (tableController.IsAtMax())
        {
            Debug.Log("MoveTrayUpButton: Table is at maximum height");

            float trayDistanceFromMax = trayController.DistanceFromMax();

            if (trayDistanceFromMax > 0 && trayDistanceFromMax >= moveAmount)
            {
                Debug.Log("MoveTrayUpButton: Tray is less than " + moveAmount + " units away from maximum");

                // Move the tray up by the specified amount
                trayController.MoveByAmount(moveAmount);
            }
            else
            {
                Debug.Log("MoveTrayUpButton: Tray is not in the correct position to move up by " + moveAmount + " units");
            }
        }
        else
        {
            Debug.Log("MoveTrayUpButton: Table is not at maximum height");
        }
    }
}
