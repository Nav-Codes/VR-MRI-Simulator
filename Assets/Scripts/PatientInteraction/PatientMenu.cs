using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatientMenu : MonoBehaviour
{
    public string iconBaseFilepath;
    public GameObject buttonPrefab;
    public GameObject menuObject;
    public Sprite cancelSprite;
    public List<PatientMenuItem> allMenuItems;

    private List<PatientMenuItem> currentMenuItems = new();
    private PatientMenuItem cancelItem;
    private bool isEnabled = false;

    void Start()
    {
        cancelItem = new PatientMenuItem();
        cancelItem.hintText = "Cancel";
        cancelItem.icon = cancelSprite;
        cancelItem.onclickCallback = (string unused) => HideMenu();
    }

    void Update()
    {
        
    }

    public void Disable()
    {
        isEnabled = false;
        ClearMenu();
    }

    public void Enable()
    {
        isEnabled = true;
    }

    public void SetItems(List<string> items, PatientMenuItem.callback stateCallback)
    {
        ClearMenu();
        if (items != null)
        {
            int buttonCount = 0;
            foreach (string item in items)
            {
                try
                {
                    AddItem(item, stateCallback, buttonCount);
                    buttonCount++;
                }
                catch(System.Exception e)
                {
                    Debug.LogWarning(e);
                }
                
            }

            currentMenuItems.Add(cancelItem);
            cancelItem.button = Instantiate(buttonPrefab, menuObject.transform).GetComponent<PatientMenuButton>();
            cancelItem.button.buttonIndex = buttonCount;
            cancelItem.button.Initialize(cancelItem);
        }
    }

    private void AddItem(string itemName, PatientMenuItem.callback stateCallback, int buttonIndex)
    {
        foreach (PatientMenuItem item in allMenuItems) 
        {
            if (item.label.Equals(itemName))
            {
                item.onclickCallback = stateCallback;
                currentMenuItems.Add(item);
                item.button = Instantiate(buttonPrefab, menuObject.transform).GetComponent<PatientMenuButton>();
                item.button.buttonIndex = buttonIndex;
                item.button.Initialize(item);
                return;
            }
        }
        throw new System.Exception($"Could not add menu item: {itemName} is not a valid menu item name");
    }

    public void ShowMenu()
    {
        if (!isEnabled) return;

        foreach (PatientMenuItem item in currentMenuItems)
        {
            item.button.AnimateIn();
        }
    }

    public void HideMenu()
    {
        if (!isEnabled) return;

        foreach (PatientMenuItem item in currentMenuItems)
        {
            item.button.AnimateOut();
        }
    }

    private void ClearMenu()
    {
        currentMenuItems.Clear();
        foreach (Transform child in menuObject.transform)
        {
            Destroy(child.gameObject);
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
    [HideInInspector] public PatientMenuButton button;
}
