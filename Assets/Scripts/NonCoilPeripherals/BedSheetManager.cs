using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// Manages the interaction and visibility of a bed sheet object in an XR environment.
/// Enables an object when grabbed and disables another object when grabbed.
/// Disables itself and enables the target object's MeshRenderer when entering a trigger collider.
/// </summary>
public class BedSheetManager : MonoBehaviour
{
    public GameObject enableObject;
    public GameObject disableObject;

    private XRGrabInteractable grabInteractable;
    public GameObject targetObject; 


    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.onSelectEntered.AddListener(OnGrab);
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        if (enableObject != null)
        {
            enableObject.SetActive(true); 
        }
        if (disableObject != null)
        {
            disableObject.SetActive(false); 
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
