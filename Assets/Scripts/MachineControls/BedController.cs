using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void MoveFixedDistance()
    {
        tableController.MoveToMax();
        trayController.MoveToMax();
        trayController.MoveToFixedDistance();
    }

    public bool IsAtMinX()
    {
        return trayController.IsAtMinX();
    }
}
