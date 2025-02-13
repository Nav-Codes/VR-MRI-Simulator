using UnityEngine;

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

    public void MoveToHome()
    {
        targetX = minX;
        targetY = maxY;
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
        } else if (!Mathf.Approximately(transform.localPosition.x, targetX))
        {
            float newX = Mathf.MoveTowards(transform.localPosition.x, targetX, speed * Time.deltaTime);
            transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
