using UnityEngine;

public class Button_Home : MonoBehaviour
{
    private TrayController trayController;
    private TableController tableController;
    private bool isReturningHome = false;

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

    private void HandleButtonPress()
    {
        if (!isReturningHome)
        {
            isReturningHome = true;
            trayController.StopMoving();
            tableController.StopMoving();
            StartCoroutine(ReturnHomeRoutine());
        }
    }

    private System.Collections.IEnumerator ReturnHomeRoutine()
    {
        // First, retract the tray
        if (!trayController.IsAtOrigin())
        {
            trayController.StartMoving(-1);
            yield return new WaitUntil(() => trayController.IsAtOrigin());
            trayController.StopMoving();
        }

        // Then, raise the table to origin
        if (!tableController.IsAtOrigin())
        {
            tableController.StartMoving(1);
            yield return new WaitUntil(() => tableController.IsAtOrigin());
            tableController.StopMoving();
        }

        isReturningHome = false;
    }

    private bool IsController(Collider other)
    {
        return other.CompareTag("PlayerHand") || other.CompareTag("Controller");
    }
}
