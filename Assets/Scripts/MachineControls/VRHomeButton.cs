using UnityEngine;
using UnityEngine.EventSystems;

public class VRHomeButton : MonoBehaviour, IPointerClickHandler
{
    public TableController tableController;
    public TrayController trayController;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!tableController.IsAtMax())
        {
            tableController.MoveToMax();
        }
        else
        {
            trayController.MoveToMin();
        }
    }
}
