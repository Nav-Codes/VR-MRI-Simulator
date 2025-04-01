using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Extends <see cref="VRHoldButton"/> to handle movement controls for a bed.
/// Determines movement direction (up or down) based on the button type.
/// When held, it moves the bed in the corresponding direction and turns on the button light.
/// </summary>
public class VRHoldMoveButton : VRHoldButton
{
    // Determines if the button moves the bed up
    private bool moveUp;
    
    // Initializes movement direction based on the button type
    void Awake()
    {
        moveUp = type == "Up";
    }
    /// <summary>
    /// Checks if the button is being held and performs the corresponding action.
    /// - If held, moves the bed and turns on the light.
    /// - If released, turns off the light.
    /// </summary>
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
    /// <summary>
    /// Moves the bed in the determined direction.
    /// - Moves up if `moveUp` is true.
    /// - Moves down otherwise.
    /// </summary>
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
