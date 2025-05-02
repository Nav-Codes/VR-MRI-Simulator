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
    public GameObject alternateObject;
    public GameObject targetObject;
    public bool switchFromStarting = false; // Flag to determine if the switch should happen from the starting object
    public bool switchFromAlternate = true; // Flag to determine if the switch should happen on the alternate object

    private void Switch()
    {
        if (startingObject.GetComponent<MeshRenderer>().enabled)
        {
            startingObject.GetComponent<MeshRenderer>().enabled = false;
            alternateObject.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            startingObject.GetComponent<MeshRenderer>().enabled = true;
            alternateObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            Switch(); // Call the Switch method when the target object enters the trigger
        }
    }

    public void SwitchLogic()
    {
        if (switchFromStarting && startingObject.GetComponent<MeshRenderer>().enabled)
        {
            Switch();
        }
        else if (switchFromAlternate && alternateObject.GetComponent<MeshRenderer>().enabled)
        {
            Switch();
        }
    }
}
