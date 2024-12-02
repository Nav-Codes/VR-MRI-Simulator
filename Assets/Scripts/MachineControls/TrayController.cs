using UnityEngine;

public class TrayController : MonoBehaviour
{
    public float minY = 0.0f; // Minimum Y position
    public float maxY = 1.0f; // Maximum Y position
    public float speed = 100.0f; // Movement speed

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

    public void MoveToMin()
    {
        targetY = minY;
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

    public void AdjustTarget()
    {
        // Move the tray in by 11 units, ensuring it doesn't exceed maxY
        float newY = transform.localPosition.y + 11f;
        newY = Mathf.Min(newY, maxY);
        MoveToPosition(newY);
    }

    public void MoveToPosition(float newY)
    {
        // Smoothly move the tray to newY
        targetY = newY;
    }

}
