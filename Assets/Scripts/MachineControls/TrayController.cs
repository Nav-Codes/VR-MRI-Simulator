using UnityEngine;
/// <summary>
/// The TrayController class is responsible for controlling the movement of a tray object within specified X and Y boundaries.
/// It provides methods to move the tray up/down (in the Y axis) and in/out (in the X axis), with speed and distance constraints.
/// The tray's movement is clamped between a minimum and maximum value for both X and Y positions, ensuring it stays within a defined range.
/// Methods like MoveToMax, MoveToHome, and MoveToFixedDistance allow for specific movement behaviors based on predefined targets.
/// The Update method ensures smooth movement by gradually updating the tray's position towards its target.
/// </summary>
public class TrayController : MonoBehaviour
{
    public float minX = 0.0f; // Minimum horizontal position
    public float maxX = 1.0f; // Maximum horizontal position    
    public float minY = 0.0f; // Minimum vertical position
    public float maxY = 1.0f; // Maximum vertical position
    public float speed = 1.0f; // Movement speed

    public float FixedDistance = 0; // Fixed movement distance for specific actions
    private float targetY; // Target Y position for movement
    private float targetX; // Target X position for movement

    // Initializes the tray's target positions to its current position.
    void Start()
    {
        targetY = transform.localPosition.y;
        targetX = transform.localPosition.x;
    }

    // Moves the tray up within the maxY boundary.
    public void MoveUp()
    {
        targetY = Mathf.Min(targetY + speed * Time.deltaTime, maxY);
    }

    // Moves the tray down within the minY boundary.
    public void MoveDown()
    {
        targetY = Mathf.Max(targetY - speed * Time.deltaTime, minY);
    }

    // Moves the tray inwards along the X-axis (towards maxX).
    public void MoveIn()
    {
        targetX = Mathf.Max(targetX - speed * Time.deltaTime, maxX);
    }
    
    // Moves the tray outwards along the X-axis (towards minX).
    public void MoveOut()
    {
        targetX = Mathf.Min(targetX + speed * Time.deltaTime, minX);
    }

    // Moves the tray to its maximum Y position.
    public void MoveToMax()
    {
        targetY = maxY;
    }
    
    // Moves the tray to its home position (max height and minimum X position).
    public void MoveToHome()
    {
        targetX = minX;
        MoveToMax();
    }

    // Checks if the tray is at its maximum Y position.
    public bool IsAtMaxY()
    {
        return Mathf.Approximately(transform.localPosition.y, maxY);
    }

    // Checks if the tray is at its minimum X position.
    public bool IsAtMinX()
    {
        return Mathf.Approximately(transform.localPosition.x, minX);
    }
    
    // Returns the remaining distance for the tray to reach max Y.
    public float DistanceFromMax()
    {
        return maxY - transform.localPosition.y;
    }

    // Moves the tray a predefined fixed distance along the X-axis.
    public void MoveToFixedDistance()
    {
        targetX = Mathf.Max(targetX + FixedDistance, maxX);
    }

    // Updates the tray's position each frame to smoothly move towards the target position.
    void Update()
    {
        // Smoothly move the tray vertically if it hasn't reached its target Y position
        if (!Mathf.Approximately(transform.localPosition.y, targetY))
        {
            float newY = Mathf.MoveTowards(transform.localPosition.y, targetY, speed * Time.deltaTime);
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        }
        // Smoothly move the tray horizontally if it hasn't reached its target X position
        else if (!Mathf.Approximately(transform.localPosition.x, targetX))
        {
            float newX = Mathf.MoveTowards(transform.localPosition.x, targetX, speed * Time.deltaTime);
            transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
