using UnityEngine;
/// <summary>
/// The EarplugSwitcher class is responsible for switching between the earplugs in the bag and worn by the patient.
/// When the earplug bag is snapped to a specific point (via the OnSnapped method from the ISnappable interface),
/// it will activate the worn earplug models and deactivate the earplug bag model.
/// </summary>
public class EarplugSwitcher : MonoBehaviour, ISnappable, CheckerInterface
{
    public GameObject earplugBag;
    public GameObject earplugsWorn;
    private bool isOnPatient = false;

    public void OnSnapped(Transform snapPointParent)
    {
        if (snapPointParent != null)
        {
            earplugsWorn = snapPointParent.Find("SnapPointEarplugs").Find("Earplugs")?.gameObject;

            if (earplugsWorn != null)
            {
                earplugsWorn.SetActive(true);
                earplugBag.SetActive(false);
                isOnPatient = true;
            }
        }
    }

    public string getLabel()
    {
        return "Earplugs on patient";
    }

    public bool isCorrect()
    {
        return isOnPatient;
    }
}
