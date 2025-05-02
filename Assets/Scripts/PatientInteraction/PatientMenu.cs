using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientMenu : MonoBehaviour
{
    public List<PatientMenuItem> AllMenuItems;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class PatientMenuItem
{
    public string Label;
    public string Text;
    public string Icon;
}
