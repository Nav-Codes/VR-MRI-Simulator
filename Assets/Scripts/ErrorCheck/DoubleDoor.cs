using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class DoubleDoor : MonoBehaviour
{
    [Header("Colliders")]
    public Collider insideRoomCollider;
    public Collider outsideRoomCollider;

    [Header("References")]
    public GameObject player;
    public GameObject patient;
    public GameObject playerCamera;
    public GameObject ErrorTextPrefab;

    [Header("Panels")]
    [SerializeField] private Transform panel1;
    [SerializeField] private Transform finalPanel1;
    [SerializeField] private Transform panel3;
    [SerializeField] private Transform finalPanel3;

    [Header("Error Checks")]
    public ErrorCheck FirstErrorCheckHolder;
    public ReturnedCheck ThirdErrorCheckHolder;

    [Header("Other")]
    public DataBanker dataBanker;
    public UnityEvent blink;
    public float blinkTime = 0.25f;

    private enum EntryStage { None, Outside, EnteredOverlapFromOutside, Inside, EnteredOverlapFromInside }
    private Dictionary<GameObject, EntryStage> objectStages = new();

    private bool playerEnteredRoomOnce = false;
    private bool bothEnteredRoomOnce = false;
    private bool playerIsInsideRoom = false;
    private bool patientIsInsideRoom = false;
    private bool firstCheckComplete = false;
    private bool thirdCheckComplete = false;

    private void Awake()
    {
        if (!insideRoomCollider || !outsideRoomCollider || !player || !patient)
        {
            Debug.LogWarning("Missing essential references in DoubleDoor script.");
        }
    }

    private void Update()
    {
        UpdateStage(player);
        UpdateStage(patient);

        if (playerIsInsideRoom && patientIsInsideRoom)
            bothEnteredRoomOnce = true;

        if (bothEnteredRoomOnce && !playerIsInsideRoom && !patientIsInsideRoom && !thirdCheckComplete)
        {
            ThirdErrorCheckHolder.Check(() =>
            {
                MovePanel(panel3, finalPanel3, "Room Teardown/Patient Dismissal Results");
                thirdCheckComplete = true;
                StartCoroutine(BlinkThenTeleport());
            }, () => { });
        }
        else if (dataBanker.GetExamType() != null && !playerIsInsideRoom && playerEnteredRoomOnce && !firstCheckComplete)
        {
            FirstErrorCheckHolder.Check(() =>
            {
                MovePanel(panel1, finalPanel1, "Room Prep Results");
                firstCheckComplete = true;
            }, () => { });
        }
    }

    #region Stage Tracking

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
                objectStages[obj] = inInside ? EntryStage.Inside : (inOutside ? EntryStage.Outside : EntryStage.None);
                break;

            case EntryStage.Outside:
                if (inOverlap) objectStages[obj] = EntryStage.EnteredOverlapFromOutside;
                break;

            case EntryStage.EnteredOverlapFromOutside:
                if (inInside && !inOutside) objectStages[obj] = EntryStage.Inside;
                else if (!inOverlap) objectStages[obj] = EntryStage.Outside;
                break;

            case EntryStage.Inside:
                if (inOverlap) objectStages[obj] = EntryStage.EnteredOverlapFromInside;
                break;

            case EntryStage.EnteredOverlapFromInside:
                if (inOutside && !inInside) objectStages[obj] = EntryStage.Outside;
                else if (!inOverlap) objectStages[obj] = EntryStage.Inside;
                break;
        }

        if (obj == player)
        {
            playerIsInsideRoom = objectStages[obj] == EntryStage.Inside;
            if (playerIsInsideRoom)
            {
                playerEnteredRoomOnce = true;
                firstCheckComplete = false;
                thirdCheckComplete = false;
            }
        }
        else if (obj == patient)
        {
            patientIsInsideRoom = objectStages[obj] == EntryStage.Inside;
        }
    }

    #endregion

    #region Panel Handling

    private void MovePanel(Transform sourcePanel, Transform targetParent, string newTitle)
    {
        if (!sourcePanel || !targetParent)
        {
            Debug.LogError("Missing panel references during move.");
            return;
        }

        // Clear any previous children in the target parent
        foreach (Transform child in targetParent)
            Destroy(child.gameObject);

        // Re-parent the entire source panel to the new parent
        sourcePanel.SetParent(targetParent, false);
        sourcePanel.localPosition = Vector3.zero;
        sourcePanel.localRotation = Quaternion.identity;
        sourcePanel.localScale = Vector3.one;

        // Make sure there's at least one child (content panel)
        if (sourcePanel.childCount == 0)
        {
            Debug.LogWarning("Source panel has no content.");
            return;
        }

        Transform contentPanel = sourcePanel.GetChild(0);

        // Replace first entry (title)
        if (contentPanel.childCount > 0)
        {
            Destroy(contentPanel.GetChild(0).gameObject);
            GameObject newTitleText = AddText(newTitle, contentPanel, Color.black, true);
            newTitleText.transform.SetSiblingIndex(0);
        }

        // Remove second entry (description)
        if (contentPanel.childCount > 1)
        {
            Destroy(contentPanel.GetChild(1).gameObject);
        }

        // Deactivate the last child of the source panel (assumed to be the button row)
        if (sourcePanel.childCount > 1)
        {
            Transform buttonRow = sourcePanel.GetChild(sourcePanel.childCount - 1);
            buttonRow.gameObject.SetActive(false);
        }
    }
    
    private GameObject AddText(string text, Transform parent, Color color, bool isTitle = false)
    {
        if (!ErrorTextPrefab) throw new MissingReferenceException("Missing ErrorTextPrefab");

        GameObject obj = Instantiate(ErrorTextPrefab, parent);
        TMP_Text tmp = obj.GetComponent<TMP_Text>();

        if (!tmp) throw new MissingComponentException("ErrorTextPrefab missing TMP_Text");

        tmp.text = isTitle ? $"<style=\"Title\">{text}</style>" : text;
        tmp.color = color;

        return obj;
    }

    #endregion

    #region Teleport & Blink

    private IEnumerator BlinkThenTeleport()
    {
        blink?.Invoke();
        yield return new WaitForSeconds(blinkTime);
        TeleportPlayer();
    }

    private void TeleportPlayer()
    {
        if (!playerCamera)
        {
            Debug.LogError("Player camera not assigned.");
            return;
        }

        float y = playerCamera.transform.position.y;
        playerCamera.transform.position = new Vector3(12.61f, y, -0.57f);
    }

    #endregion
}
