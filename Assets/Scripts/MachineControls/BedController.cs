using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class controls the movement and positioning of the bed, including the tray, table, and cart.
/// It interacts with the <see cref="TableController"/>, <see cref="TrayController"/>, and <see cref="CartMovementController"/> to move these components 
/// based on specific conditions, such as whether the tray is at its maximum height or whether the cart is docked.
/// Methods include moving the bed up or down, resetting to home position, and moving the tray and table to fixed distances or maximum positions.
/// </summary>
public class BedController : MonoBehaviour
{
    public TableController tableController;
    public TrayController trayController;
    public CartMovementController cartMovementController;
    public void MoveUp()
    {
        // for moving up, if the table/tray is not at max height, move both tray and table up
        if (!trayController.IsAtMaxY())
        {
            trayController.MoveUp();
            tableController.MoveUp();
        }
        else if (!cartMovementController.isDocked()) {
            // if the tray is at max height and the cart is not docked, do nothing
        }
        else // if the tray is at max height, move the tray into mri
        {
            trayController.MoveIn();
        }
    }

    public void MoveDown()
    {
        //if the tray is outside the mri, move tray and table down
        if (trayController.IsAtMinX())
        {
            tableController.MoveDown();
            trayController.MoveDown();
        }
        else
        { // if the tray is inside the mri, move both tray out
            trayController.MoveOut();
        }
    }

    public void HomePosition() // home position is max height for table and tray outside mri
    {
        tableController.MoveToMax();
        trayController.MoveToHome();
    }

    public void MoveFixedDistance() // Approximately half way into the MRI
    {
        tableController.MoveToMax();
        trayController.MoveToMax();
        trayController.MoveToFixedDistance();
    }

    public bool IsAtMinX() // x is the horizontal axis into the MRI
    {
        return trayController.IsAtMinX();
    }
}
