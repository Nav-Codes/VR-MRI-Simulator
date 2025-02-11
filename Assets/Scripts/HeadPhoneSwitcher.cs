using UnityEngine;
// Used to switch between the open and closed states of the headphones
public class HeadPhoneSwitcher : MonoBehaviour, ISnappable
{
    public GameObject headphoneClosed;
    public GameObject headphoneOpen;
    public void OnSnapped(Transform snapPointParent)
    {
        if (snapPointParent != null)
        {
            headphoneOpen = snapPointParent.Find("SnapPointHeadphone").Find("Headphone_Open")?.gameObject;

            if (headphoneOpen != null)
            {
                headphoneOpen.SetActive(true);
                headphoneClosed.SetActive(false);
            }
        }


    }

}
