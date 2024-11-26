using UnityEngine;

public class Button_Down : MonoBehaviour
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
        if (!trayController.IsAtOrigin())
        {
            trayController.StartMoving(-1); // Retract the tray
        }
        else if (!tableController.IsAtLowestPoint())
        {
            tableController.StartMoving(-1); // Lower the table
        }
    }

    private bool IsController(Collider other)
    {
        return other.CompareTag("PlayerHand") || other.CompareTag("Controller");
    }
}
