using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// Manages the interaction and visibility of a bed sheet object in an XR environment.
/// Enables a child object when grabbed.
/// Disables itself and enables the target object's MeshRenderer when entering a trigger collider.
/// </summary>
public class BedSheetManager : MonoBehaviour
{
    public GameObject childObject;

    private XRGrabInteractable grabInteractable;
    public GameObject targetObject; 


    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.onSelectEntered.AddListener(OnGrab);
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        if (childObject != null)
        {
            childObject.SetActive(true); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            gameObject.SetActive(false); 
            targetObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
