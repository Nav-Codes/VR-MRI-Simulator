using UnityEngine;

public class Button_Up : MonoBehaviour
{
    private TrayController trayController;
    private TableController tableController;

    void Start()
    {
        trayController = FindObjectOfType<TrayController>();
        tableController = FindObjectOfType<TableController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsController(other))
        {
            HandleButtonPress();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsController(other))
        {
            trayController.StopMoving();
            tableController.StopMoving();
        }
    }

    private void HandleButtonPress()
    {
        if (!tableController.IsAtOrigin())
        {
            tableController.StartMoving(1); // Raise the table
        }
        else if (!trayController.IsFullyExtended())
        {
            trayController.StartMoving(1); // Extend the tray
        }
    }

    private bool IsController(Collider other)
    {
        return other.CompareTag("PlayerHand") || other.CompareTag("Controller");
    }
}
