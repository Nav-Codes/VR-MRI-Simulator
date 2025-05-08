using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientMenuButton : MonoBehaviour
{
    public int buttonIndex;
    private Vector3 endPosition;

    public void Initialize(PatientMenuItem menuItem)
    {
        GetComponent<UnityEngine.UI.Button>()
            .onClick.AddListener(delegate { menuItem.onclickCallback(menuItem.targetStateLabel); });
        if (menuItem.icon != null)
        {
            GetComponent<UnityEngine.UI.Image>().sprite = menuItem.icon;
        }
        endPosition = new Vector3(0, -1.65f * buttonIndex, 0);
        gameObject.SetActive(false);
    }

    public void AnimateIn()
    {
        transform.localPosition = endPosition;
        gameObject.SetActive(true);
    }

    public void AnimateOut() 
    {
        gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
    }
}
