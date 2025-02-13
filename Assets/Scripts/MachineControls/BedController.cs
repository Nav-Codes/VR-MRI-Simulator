using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : MonoBehaviour
{
    public TableController tableController;
    public TrayController trayController;

    public void MoveUp()
    {
        // for moving up, if the table/tray is not at max height, move both tray and table up
        if (!trayController.IsAtMaxY())
        {
            trayController.MoveUp();
            tableController.MoveUp();
        }
        else // if the tray is at max height, move the tray into mri
        {
            trayController.MoveIn();
        }
    }

    public void MoveDown()
    {
        // for moving down, if the tray is inside the mri, move tray out
        if (!trayController.IsAtMinX())
        {
            trayController.MoveOut();
        } else { // if the tray is not inside the mri, move both tray and table down
            tableController.MoveDown();
            trayController.MoveDown();
        }
    }

    public void HomePosition() // home position is max height for table and tray outside mri
    {
        tableController.MoveToMax();
        trayController.MoveToHome();
    }

    public void MoveFixedDistance() {
        tableController.MoveToMax();
        trayController.MoveToMax();
        trayController.MoveToFixedDistance();
    }
}
