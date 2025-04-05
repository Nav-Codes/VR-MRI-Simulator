using UnityEngine;
/// <summary>
/// The HeadPhoneSwitcher class is responsible for switching between the open and closed states of the headphones.
/// When the headphones are snapped to a specific point (via the OnSnapped method from the ISnappable interface),
/// it will activate the open headphone model and deactivate the closed one.
/// </summary>
public class HeadPhoneSwitcher : MonoBehaviour, ISnappable, CheckerInterface
{
    public GameObject headphoneClosed;
    public GameObject headphoneOpen;
    private bool isOnPatient = false;

    public void OnSnapped(Transform snapPointParent)
    {
        if (snapPointParent != null)
        {
            headphoneOpen = snapPointParent.Find("SnapPointHeadphone").Find("Headphone_Open")?.gameObject;
            //Transform headphoneOpen = snapPointParent.Find("SnapPointHeadphone");
            //Debug.Log("headpohneOpen: " +  headphoneOpen);

            if (headphoneOpen != null)
            {
                headphoneOpen.SetActive(true);
                headphoneClosed.SetActive(false);
                isOnPatient = true;
            }
        }
    }

    public string getLabel()
    {
        return "Headphones on patient";
    }

    public bool isCorrect()
    {
        return isOnPatient;
    }
}
