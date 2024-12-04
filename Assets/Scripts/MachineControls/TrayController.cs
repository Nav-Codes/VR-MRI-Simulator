using UnityEngine;

public class TrayController : MonoBehaviour
{
    public float minY = 0.0f; // Minimum Y position
    public float maxY = 1.0f; // Maximum Y position
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

    public void MoveByAmount(float amount)
    {
        targetY = Mathf.Clamp(transform.localPosition.y + amount, minY, maxY);
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

    public float DistanceFromMax()
    {
        return maxY - transform.localPosition.y;
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
