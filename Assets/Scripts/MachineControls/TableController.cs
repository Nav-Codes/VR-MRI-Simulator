using UnityEngine;
/// <summary>
/// This class controls the vertical movement of a table in a VR environment.
/// It allows the table to move between a specified minimum and maximum Y position at a defined speed.
/// The table can be moved up, down, or directly to its maximum position, and it will smoothly transition between positions when moved.
/// The class also provides methods to check if the table is at its minimum or maximum position.
/// </summary>
public class TableController : MonoBehaviour
{
    public float minY = 0.0f; // Minimum Y position
    public float maxY = 2.0f; // Maximum Y position
    public float speed = 1.0f; // Movement speed

    private float targetY;

    void Start()
    {
        targetY = transform.localPosition.y;
    }

    public void MoveUp()
    {
        targetY = Mathf.Min(targetY + speed * Time.deltaTime, maxY);
    }

    public void MoveDown()
    {
        targetY = Mathf.Max(targetY - speed * Time.deltaTime, minY);
    }

    public void MoveToMax()
    {
        targetY = maxY;
    }

    public bool IsAtMax()
    {
        return Mathf.Approximately(transform.localPosition.y, maxY);
    }

    public bool IsAtMin()
    {
        return Mathf.Approximately(transform.localPosition.y, minY);
    }

    void Update()
    {
        if (!Mathf.Approximately(transform.localPosition.y, targetY))
        {
            float newY = Mathf.MoveTowards(transform.localPosition.y, targetY, speed * Time.deltaTime);
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        }
    }
}
