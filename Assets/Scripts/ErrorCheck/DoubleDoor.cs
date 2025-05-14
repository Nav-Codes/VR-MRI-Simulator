using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.XR.Management;
using Unity.XR.CoreUtils;

public class DoubleDoor : MonoBehaviour
{
    public Collider insideRoomCollider;
    public Collider outsideRoomCollider;
    public GameObject player;
    public GameObject patient;

    [SerializeField] private Transform panel1ToCopy;  // Source panel to copy from
    [SerializeField] private Transform Final1stPanel; // Destination panel to copy to
    [SerializeField] private Transform panel3ToCopy;  // Source panel to copy from 
    [SerializeField] private Transform Final3rdPanel; // Destination panel to copy to
    [SerializeField] private GameObject ErrorTextPrefab;


    private enum EntryStage
    {
        None,
        Outside,
        EnteredOverlapFromOutside,
        Inside,
        EnteredOverlapFromInside
    }

    private Dictionary<GameObject, EntryStage> objectStages = new Dictionary<GameObject, EntryStage>();
    public DataBanker dataBanker = null;
    private bool playerIsInsideRoom = false;
    private bool patientIsInsideRoom = false;
    private bool bothEnteredRoomOnce = false;
    private bool playerEnteredRoomOnce = false;
    public ErrorCheck FirstErrorCheckHolder = null;
    public ReturnedCheck ThirdErrorCheckHolder = null;
    private bool FirstErrorCheckClosed = false;
    private bool FirstErrorCheckClosedForever = false;
    private bool ThirdErrorCheckClosed = false;
    private bool ThirdErrorCheckClosedForever = false;

    public GameObject playerCamera;

    private void Awake()
    {
        if (insideRoomCollider == null || outsideRoomCollider == null || player == null || patient == null)
        {
            Debug.LogWarning("Missing references in DoubleDoor script.");
        }
    }

    private bool IsErrorText(GameObject obj)
    {
        // Check if this is the specific error text we want to ignore
        TMP_Text textComponent = obj.GetComponent<TMP_Text>();
        if (textComponent == null) return false;

        return textComponent.text.Contains("Procedure Feedback") ||
               textComponent.text.Contains("Please fix your errors before continuing");
    }

    private GameObject AddText(string text, Transform Panel, Color color, bool isTitle = false)
    {
        GameObject errorTextObj = Instantiate(ErrorTextPrefab, Panel);
        LayoutRebuilder.ForceRebuildLayoutImmediate(Panel.GetComponent<RectTransform>());

        TMP_Text errorText = errorTextObj.GetComponent<TMP_Text>();
        if (errorText == null)
        {
            throw new Exception("ErrorTextPrefab missing TMP_Text component");
        }

        errorText.text = isTitle ? $"<style=\"Title\">{text}</style>" : text;
        errorText.color = color;

        return errorTextObj;
    }

    private void CopyFirstDisplay()
    {
        if (panel1ToCopy == null || Final1stPanel == null)
        {
            Debug.LogError("First panel references are missing!");
            return;
        }

        // Clear existing children in destination
        foreach (Transform child in Final1stPanel)
        {
            Destroy(child.gameObject);
        }

        AddText("First Check Results", Final1stPanel, Color.black, true);

        foreach (Transform child in panel1ToCopy)
        {
            // Copy all children from source to destination
            if (!IsErrorText(child.gameObject))
            {
                GameObject newChild = Instantiate(child.gameObject, Final1stPanel);
            }

            Debug.Log($"Copied children from {panel1ToCopy.name} to {Final1stPanel.name}");
        }
    }

    private void CopyThirdDisplay()
    {
        if (panel3ToCopy == null || Final3rdPanel == null)
        {
            Debug.LogError("Third panel references are missing!");
            return;
        }

        // Clear existing children in destination
        foreach (Transform child in Final3rdPanel)
        {
            Destroy(child.gameObject);
        }

        AddText("Third Check Results", Final3rdPanel, Color.black, true);
        foreach (Transform child in panel3ToCopy)
        {
            // Copy all children from source to destination
            if (!IsErrorText(child.gameObject))
            {
                GameObject newChild = Instantiate(child.gameObject, Final3rdPanel);
                newChild.name = child.name; // Preserve original name
            }

            Debug.Log($"Copied children from {panel3ToCopy.name} to {Final3rdPanel.name}");
        }
    }


    private void TeleportPlayer()
    {
        if (playerCamera != null)
        {
            // Get the current Y value to preserve height
            float currentY = playerCamera.transform.position.y;

            // Set a new position using the existing Y
            playerCamera.transform.position = new Vector3(12.61f, currentY, -0.57f);
        }
        else
        {
            Debug.LogError("Current player camera not found for teleportation!");
        }
    }




    private void Update()
    {
        UpdateStage(player);
        UpdateStage(patient);
        if (playerIsInsideRoom && patientIsInsideRoom)
        {
            bothEnteredRoomOnce = true;
        }
        if (bothEnteredRoomOnce && !playerIsInsideRoom && !patientIsInsideRoom && !ThirdErrorCheckClosed && !ThirdErrorCheckClosedForever)
        {
            ThirdErrorCheckHolder.Check(() =>
            {
                CopyThirdDisplay();
                ThirdErrorCheckClosedForever = true;
                TeleportPlayer();
            }, () => { ThirdErrorCheckClosed = true; });
        }
        else if (dataBanker.GetExamType() != null && !playerIsInsideRoom && playerEnteredRoomOnce && !FirstErrorCheckClosed && !FirstErrorCheckClosedForever)
        {
            FirstErrorCheckHolder.Check(() =>
            {
                CopyFirstDisplay();
                FirstErrorCheckClosedForever = true;
            }, () => { FirstErrorCheckClosed = true; });
        }
    }



    private void UpdateStage(GameObject obj)
    {
        if (!objectStages.ContainsKey(obj))
            objectStages[obj] = EntryStage.None;

        Vector3 pos = obj.transform.position;
        bool inOutside = outsideRoomCollider.bounds.Contains(pos);
        bool inInside = insideRoomCollider.bounds.Contains(pos);
        bool inOverlap = inOutside && inInside;

        EntryStage current = objectStages[obj];

        switch (current)
        {
            case EntryStage.None:
                if (inInside && !inOutside)
                    objectStages[obj] = EntryStage.Inside;
                else if (inOutside && !inInside)
                    objectStages[obj] = EntryStage.Outside;
                break;

            case EntryStage.Outside:
                if (inOverlap)
                    objectStages[obj] = EntryStage.EnteredOverlapFromOutside;
                break;

            case EntryStage.EnteredOverlapFromOutside:
                if (inInside && !inOutside)
                    objectStages[obj] = EntryStage.Inside;
                else if (!inOverlap)
                    objectStages[obj] = EntryStage.Outside; // backed out
                break;

            case EntryStage.Inside:
                if (inOverlap)
                    objectStages[obj] = EntryStage.EnteredOverlapFromInside;
                break;

            case EntryStage.EnteredOverlapFromInside:
                if (inOutside && !inInside)
                    objectStages[obj] = EntryStage.Outside;
                else if (!inOverlap)
                    objectStages[obj] = EntryStage.Inside; // backed in
                break;
        }

        // Final state check
        if (obj == player)
        {
            playerIsInsideRoom = objectStages[obj] == EntryStage.Inside;
            if (playerIsInsideRoom)
            {
                playerEnteredRoomOnce = true;
                FirstErrorCheckClosed = false;
                ThirdErrorCheckClosed = false;
            }
        }
        else if (obj == patient)
            patientIsInsideRoom = objectStages[obj] == EntryStage.Inside;
    }

}
