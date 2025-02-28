using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// This class controls the movement of a cart in a VR environment based on the input from left and right hand controllers.
/// It ensures that the cart moves only when both handles are grabbed and the required preconditions (such as power being on and park button off) are met.
/// The class uses Rigidbody physics to apply movement forces based on the difference in controller positions. It also tracks docking states and manages the activation of related button lights when the cart reaches certain targets.
/// </summary>
public class CartMovementController : MonoBehaviour
{
    public Transform leftControllerTransform;
    public Transform rightControllerTransform;
    public HandleXRGrabInteraction leftHandle;
    public HandleXRGrabInteraction rightHandle;
    public Rigidbody cartRigidbody; 
    public ToggleLightButton powerButton;
    public ToggleLightButton parkButton;
    public BasicLightButton dockButton;
    private Vector3 lastLeftPos;
    private Vector3 lastRightPos;
    private bool initialized = false; // Tracks if positions have been initialized
    private void FixedUpdate()
    {
        bool leftGrabbed = leftHandle.isHandleGrabbed();
        bool rightGrabbed = rightHandle.isHandleGrabbed();

        if (leftGrabbed && rightGrabbed && checkMovePreconditions())
        {
            MoveCart();
        }
        else
        {
            initialized = false; // Reset when handles are released
        }
    }

    private void MoveCart()
    {
        // Get the current positions of the controllers
        Vector3 leftCurrentPos = leftControllerTransform.position;
        Vector3 rightCurrentPos = rightControllerTransform.position;

        // Initialize previous positions on first movement
        if (!initialized)
        {
            lastLeftPos = leftCurrentPos;
            lastRightPos = rightCurrentPos;
            initialized = true;
            return; // Skip first frame to avoid sudden jump
        }

        // Calculate movement direction by averaging both controller movements
        Vector3 leftMovement = leftCurrentPos - lastLeftPos;
        Vector3 rightMovement = rightCurrentPos - lastRightPos;
        Vector3 averageMovement = (leftMovement + rightMovement) / 2f;

        // Convert movement to cart's local space (so it moves forward/backward)
        Vector3 localMovement = transform.InverseTransformDirection(averageMovement);
        localMovement.y = 0f;
        localMovement.z = 0f;

        // Apply force instead of setting velocity directly
        cartRigidbody.AddForce(transform.TransformDirection(localMovement) * 4000f, ForceMode.Acceleration);

        // Store last positions for next frame
        lastLeftPos = leftCurrentPos;
        lastRightPos = rightCurrentPos;
    }

    private bool checkMovePreconditions()
    {
        return powerButton.getState() && !parkButton.getState();
    }

    public bool isDocked()
    {
        return dockButton.getState();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the cart reaches a the mri
        if (other.CompareTag("MriBody")&&!parkButton.getState()) 
        {
            dockButton.TurnOn();
            parkButton.TurnOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detect when the cart leaves the target area
        if (other.CompareTag("MriBody")) 
        {
            dockButton.TurnOff();
        }
    }
}
