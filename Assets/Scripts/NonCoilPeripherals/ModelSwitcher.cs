using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// Manages the interaction and visibility of a object in an XR environment.
/// Enables an object when grabbed and disables another object when grabbed.
/// Disables itself and enables the target object's MeshRenderer when entering a trigger collider.
/// </summary>
public class ModelSwitcher : MonoBehaviour
{
    public GameObject startingObject;
    public Collider startingCollider = null; // Collider for the starting object
    public GameObject alternateObject;
    public Collider alternateCollider = null; // Collider for the alternate object
    public GameObject targetObject;
    public bool switchFromStarting = false; // Flag to determine if the switch should happen from the starting object
    public bool switchFromAlternate = true; // Flag to determine if the switch should happen on the alternate object
    private void Switch()
    {
        if (startingObject.activeSelf)
        {
            startingObject.SetActive(false);
            if (startingCollider != null)
                startingCollider.enabled = false; // Disable the collider for the starting object
            alternateObject.SetActive(true);
            if (alternateCollider != null)
                alternateCollider.enabled = true;
        }
        else
        {
            alternateObject.SetActive(false);
            if (alternateCollider != null)
                alternateCollider.enabled = false;
            startingObject.SetActive(true);
            if (startingCollider != null)
                startingCollider.enabled = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject && !switchFromStarting)
        {
            Switch(); // Call the Switch method when the target object enters the trigger
        }
    }

    public void SwitchLogic()
    {
        if (switchFromStarting && startingObject.activeSelf)
        {
            Switch();
        }
        else if (switchFromAlternate && alternateObject.activeSelf)
        {
            Switch();
        }
    }
}
