using UnityEngine;

/// <summary>
/// The TrayController class controls the movement of a tray object within defined X and Y boundaries.
/// It supports separate speeds for X and Y movement, includes methods for specific tray behaviors,
/// and prevents interference during special moves.
/// </summary>
public class TrayController : MonoBehaviour
{
    public float minX = 0.0f;
    public float maxX = -1.0f;
    public float minY = 0.0f;
    public float maxY = 1.0f;
    private float ySpeed = 1.0f;
    private float xSpeed = 1.0f;
    public TableController tableController;
    public float FixedDistance = 0;
    private float targetY;
    private float targetX;
    private bool isMovingHome = false;
    private bool isMovingToFixedDistance = false;
    private bool IsSpecialMovementActive => isMovingHome || isMovingToFixedDistance;

    void Start()
    {
        targetY = transform.localPosition.y;
        targetX = transform.localPosition.x;
        ySpeed = tableController.speed;
        xSpeed = tableController.speed;
    }

    public void MoveUp()
    {
        if (IsSpecialMovementActive) return;
        targetY = Mathf.Min(targetY + ySpeed * Time.deltaTime, maxY);
    }

    public void MoveDown()
    {
        if (IsSpecialMovementActive) return;
        targetY = Mathf.Max(targetY - ySpeed * Time.deltaTime, minY);
    }

    public void MoveIn()
    {
        if (IsSpecialMovementActive) return;
        targetX = Mathf.Max(targetX - xSpeed * Time.deltaTime, maxX);
    }

    public void MoveOut()
    {
        if (IsSpecialMovementActive) return;
        targetX = Mathf.Min(targetX + xSpeed * Time.deltaTime, minX);
    }

    public void MoveToMax()
    {
        targetY = maxY;
    }

    public void MoveToHome()
    {
        targetX = minX;
        MoveToMax();
        isMovingHome = true;
        isMovingToFixedDistance = false;  
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
        targetX = Mathf.Max(targetX - Mathf.Abs(FixedDistance), maxX);
        isMovingToFixedDistance = true;
        isMovingHome = false;  
    }

    void Update()
    {
        Vector3 currentPos = transform.localPosition;

        // Always use normal Y speed
        float newY = Mathf.MoveTowards(currentPos.y, targetY, ySpeed * Time.deltaTime);

        // Use doubled X speed during special moves
        float currentXSpeed = (isMovingHome || isMovingToFixedDistance) ? xSpeed * 2f : xSpeed;
        float newX = Mathf.MoveTowards(currentPos.x, targetX, currentXSpeed * Time.deltaTime);

        transform.localPosition = new Vector3(newX, newY, currentPos.z);

        // Stop special move flag when targets are reached
        if (isMovingHome && Mathf.Approximately(newX, targetX) && Mathf.Approximately(newY, targetY))
        {
            isMovingHome = false;
        }

        if (isMovingToFixedDistance && Mathf.Approximately(newX, targetX))
        {
            isMovingToFixedDistance = false;
        }
    }
}
