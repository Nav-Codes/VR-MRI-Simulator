using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Extends <see cref="VRHoldButton"/> to handle the VR home button functionality.
/// When held, it moves the bed to the home position and turns on the corresponding button light.
/// </summary>
public class VRHomeButton : VRHoldButton
{
    protected override void ifHeld()
    {
        bedController.HomePosition();
        LightOn();
    }
}
