using UnityEngine;

public class TableController : MonoBehaviour
{
    public float speed = 0.1f;
    public float minY;
    public float maxY;

    private bool isMoving = false;
    private int direction = 0;

    void Update()
    {
        if (isMoving)
        {
            MoveTable();
        }
    }

    public bool IsAtOrigin()
    {
        return Mathf.Approximately(transform.position.y, maxY);
    }

    public bool IsAtLowestPoint()
    {
        return Mathf.Approximately(transform.position.y, minY);
    }

    public void StartMoving(int moveDirection)
    {
        direction = moveDirection;
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    private void MoveTable()
    {
        float deltaY = speed * Time.deltaTime * direction;
        float newY = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        if (newY == minY || newY == maxY)
        {
            StopMoving();
        }
    }
}
