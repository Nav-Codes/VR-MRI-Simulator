using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Extends <see cref="VRHoldButton"/> to handle movement controls for a bed.
/// Determines movement direction (up or down) based on the button type.
/// When held, it moves the bed in the corresponding direction and turns on the button light.
/// </summary>
public class VRHoldMoveButton : VRHoldButton
{
    private bool moveUp;
    void Awake()
    {
        moveUp = type == "Up";
    }
    private void Update()
    {
        if (isHeld)
        {
            Move();
            LightOn();
        }
        else
        {
            LightOff();
        }
    }

    private void Move()
    {
        if (moveUp)
        {
            bedController.MoveUp();
        }
        else
        {
            bedController.MoveDown();
        }
    }
}
