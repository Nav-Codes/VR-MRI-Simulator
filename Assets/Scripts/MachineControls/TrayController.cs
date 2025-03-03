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
    public float minX = 0.0f; // Minimum X position
    public float maxX = 1.0f; // Maximum X position    
    public float minY = 0.0f; // Minimum Y position
    public float maxY = 1.0f; // Maximum Y position
    public float speed = 1.0f; // Movement speed

    public float FixedDistance = 0;
    private float targetY;
    private float targetX;

    void Start()
    {
        targetY = transform.localPosition.y;
        targetX = transform.localPosition.x;
    }

    public void MoveUp()
    {
        targetY = Mathf.Min(targetY + speed * Time.deltaTime, maxY);
    }

    public void MoveDown()
    {
        targetY = Mathf.Max(targetY - speed * Time.deltaTime, minY);
    }

    public void MoveIn()
    {
        targetX = Mathf.Max(targetX - speed * Time.deltaTime, maxX);
    }

    public void MoveOut()
    {
        targetX = Mathf.Min(targetX + speed * Time.deltaTime, minX);
    }

    public void MoveToMax()
    {
        targetY = maxY;
    }
    public void MoveToHome()
    {
        targetX = minX;
        MoveToMax();
    }

    public bool IsAtMaxY()
    {
        return Mathf.Approximately(transform.localPosition.y, maxY);
    }

    public bool IsAtMinX()
    {
        return Mathf.Approximately(transform.localPosition.x, minX);
    }

    public float DistanceFromMax()
    {
        return maxY - transform.localPosition.y;
    }

    public void MoveToFixedDistance()
    {
        targetX = Mathf.Max(targetX + FixedDistance, maxX);
    }

    void Update()
    {
        if (!Mathf.Approximately(transform.localPosition.y, targetY))
        {
            float newY = Mathf.MoveTowards(transform.localPosition.y, targetY, speed * Time.deltaTime);
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        }
        else if (!Mathf.Approximately(transform.localPosition.x, targetX))
        {
            float newX = Mathf.MoveTowards(transform.localPosition.x, targetX, speed * Time.deltaTime);
            transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
