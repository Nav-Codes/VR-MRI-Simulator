using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLightController : MonoBehaviour
{
    public GameObject upButtonLight;
    public GameObject downButtonLight;

    public void UpButtonTurnOn()
    {
        upButtonLight.SetActive(true);
    }

    public void UpButtonTurnOff()
    {
        upButtonLight.SetActive(false);
    }

    public void DownButtonTurnOn()
    {
        downButtonLight.SetActive(true);
    }

    public void DownButtonTurnOff()
    {
        downButtonLight.SetActive(false);
    }
}
