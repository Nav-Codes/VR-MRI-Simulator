using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knob : MonoBehaviour
{
    // Make public variables that can be set in the Unity Editor
    public Transform targetObject; // the target object to move
    public Vector3 targetAxis; // axis along which to move targetObject
    public float minAngle = -90;
    public float maxAngle = 90;
    public float min;
    public float max;
    public GameObject[] disableConditionsGO; // game objects that disable knob if active

    private bool knobEnabled = true;

    private Vector3 initTargetPosition;

    // Start is called before the first frame update
    void Start()
    {
        // save the initial position of the target object
        initTargetPosition = new Vector3(targetObject.position.x, targetObject.position.y, targetObject.position.z);
    }

    // Temp mouse input for testing only 
    // -- this input should be replaced with VR controller input
    // a simple method to update the know rotation based on mouse over
    private void OnMouseOver() 
    {
        if (GetKnobEnabled())
        {
            UpdateKnobRotation();
            UpdateTargetPosition();
        }
        

    }

    // Update Knob Rotation 
    private void UpdateKnobRotation()
    {
        // TODO: this sample code uses MOUSE input -- update for VR input controls
        //get drag direction left or right
        float dragDirection = Input.GetAxis("Mouse X");
        //rotate knob with min and max angle
        if (dragDirection > 0)
        {
            if (gameObject.transform.localEulerAngles.z < maxAngle )
            {
                gameObject.transform.Rotate(0, 0, 1);
            }
                   }
        else if (dragDirection < 0)
        {
            // NB clamp rotation to positive values
            if (gameObject.transform.localEulerAngles.z > minAngle && gameObject.transform.localEulerAngles.z > 1)
            {
                gameObject.transform.Rotate(0, 0, -1);
            }
        }
    }


    // Update Target Position based on knob angle
    private void UpdateTargetPosition()
    {
        // move targetObject along targetAxis based on the rotation of the knob
        float angle = Mathf.Clamp(gameObject.transform.localEulerAngles.z, minAngle, maxAngle);
        float value = angle / (maxAngle - minAngle) * (max - min);
        targetObject.localPosition = initTargetPosition + targetAxis * value;
    }

    // check if know is enabled
    private bool GetKnobEnabled()
    {
        knobEnabled = true;
        // if any of the disableConditionsGO are active, disable the knob
        // if all of the disableConditionsGO are inactive, enable the knob
        foreach (GameObject go in disableConditionsGO)
        {
            if (go.activeSelf)
            {
                knobEnabled = false;
                return false;
            }
        }

        return knobEnabled;
        
    }
}
