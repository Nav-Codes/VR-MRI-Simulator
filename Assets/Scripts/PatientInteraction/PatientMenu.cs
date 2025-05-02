using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientMenu : MonoBehaviour
{
    public List<PatientMenuItem> allMenuItems;

    private List<PatientMenuItem> currentMenuItems = new List<PatientMenuItem>();
    private bool isEnabled = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Disable()
    {
        isEnabled = false;
    }

    public void Enable()
    {
        isEnabled = true;
    }

    public void SetItems(List<string> items)
    {
        if (items != null)
        {
            foreach (string item in items)
            {
                try
                {
                    AddItem(item);
                }
                catch(System.Exception e)
                {
                    Debug.LogWarning(e);
                }
                
            }
        }
    }

    private void AddItem(string itemName)
    {
        foreach (PatientMenuItem item in allMenuItems) 
        {
            if (item.label.Equals(itemName))
            {
                currentMenuItems.Add(item);
                return;
            }
        }
        throw new System.Exception($"Could not add menu item: {itemName} is not a valid menu item name");
    }

    public void ShowMenu()
    {
        foreach (PatientMenuItem item in currentMenuItems)
            Debug.Log("MENU ITEM: " + item.label);
    }
}

[System.Serializable]
public class PatientMenuItem
{
    public string label;
    public string targetStateLabel;
    public string text;
    public string icon;
}
