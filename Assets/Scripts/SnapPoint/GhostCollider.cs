using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollider : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;        
    }
}
