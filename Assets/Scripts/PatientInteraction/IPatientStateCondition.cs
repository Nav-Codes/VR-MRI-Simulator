using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPatientStateCondition
{
    public bool IsStateChangeAllowed();

    public string GetRefusalMessage();
}
