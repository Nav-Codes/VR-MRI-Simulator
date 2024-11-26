using UnityEngine;

public class TrayController : MonoBehaviour
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
            MoveTray();
        }
    }

    public bool IsAtOrigin()
    {
        return Mathf.Approximately(transform.localPosition.y, minY);
    }

    public bool IsFullyExtended()
    {
        return Mathf.Approximately(transform.localPosition.y, maxY);
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

    private void MoveTray()
    {
        float deltaY = speed * Time.deltaTime * direction;
        float newY = Mathf.Clamp(transform.localPosition.y + deltaY, minY, maxY);
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);

        if (newY == minY || newY == maxY)
        {
            StopMoving();
        }
    }
}
