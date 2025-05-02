using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatientMenu : MonoBehaviour
{
    public string iconBaseFilepath;
    public GameObject buttonPrefab;
    public GameObject menuObject;
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

    public void SetItems(List<string> items, PatientMenuItem.callback stateCallback)
    {
        if (items != null)
        {
            foreach (string item in items)
            {
                try
                {
                    AddItem(item, stateCallback);
                }
                catch(System.Exception e)
                {
                    Debug.LogWarning(e);
                }
                
            }
        }
    }

    private void AddItem(string itemName, PatientMenuItem.callback stateCallback)
    {
        foreach (PatientMenuItem item in allMenuItems) 
        {
            if (item.label.Equals(itemName))
            {
                item.onclickCallback = stateCallback;
                currentMenuItems.Add(item);
                return;
            }
        }
        throw new System.Exception($"Could not add menu item: {itemName} is not a valid menu item name");
    }

    public void ShowMenu()
    {
        int count = 0;
        foreach (PatientMenuItem item in currentMenuItems)
        {
            GameObject itemButton = Instantiate(buttonPrefab, menuObject.transform);
            itemButton.GetComponent<UnityEngine.UI.Button>()
                .onClick.AddListener(delegate { item.onclickCallback(item.targetStateLabel); });
            if (item.icon != null)
            {
                itemButton.GetComponent<UnityEngine.UI.Image>().sprite = item.icon;
                itemButton.transform.localPosition = new Vector3(0, -1.65f * count, 0);
                count++;
            }
        }
    }
}

[System.Serializable]
public class PatientMenuItem
{
    public string label;
    public string targetStateLabel;
    public string hintText;
    public Sprite icon;
    [HideInInspector] public delegate void callback(string label);
    [HideInInspector] public callback onclickCallback;
}
