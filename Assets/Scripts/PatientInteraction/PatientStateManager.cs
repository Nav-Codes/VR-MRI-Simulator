using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientStateManager : MonoBehaviour
{
    public GameObject Patient;
    public PatientMenu PatientMenu;
    public List<PatientState> PatientStates;


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
public class PatientState
{
    public string label;
    public List<PatientMenuItem> menuItems;
    public StateBeginTransition transition;
}

[System.Serializable]
public class StateBeginTransition
{
    public string? animationName;
    public GameObject? parent;
    public string? movementLabel;
}
