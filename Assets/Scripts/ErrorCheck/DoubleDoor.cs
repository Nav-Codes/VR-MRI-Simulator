using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoubleDoor : MonoBehaviour
{
    public Collider insideRoomCollider;
    public Collider outsideRoomCollider;
    public GameObject player;
    public GameObject patient;

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

    private void Awake()
    {
        if (insideRoomCollider == null || outsideRoomCollider == null || player == null || patient == null)
        {
            Debug.LogWarning("Missing references in DoubleDoor script.");
        }
    }

    private void Update()
    {
        UpdateStage(player);
        UpdateStage(patient);
        if (playerIsInsideRoom && patientIsInsideRoom){
            bothEnteredRoomOnce = true;
        }
        if (bothEnteredRoomOnce && !playerIsInsideRoom && !patientIsInsideRoom)
        {
            ThirdErrorCheckHolder.Check(() => {}, () => {});
        } else if (dataBanker.GetExamType() != null && !playerIsInsideRoom && playerEnteredRoomOnce)
        {
            FirstErrorCheckHolder.Check(() => {}, () => {});
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
        if (obj == player) {
            playerIsInsideRoom = objectStages[obj] == EntryStage.Inside;
            if (playerIsInsideRoom && !playerEnteredRoomOnce)
            {
                playerEnteredRoomOnce = true;
            }
        }
        else if (obj == patient)
            patientIsInsideRoom = objectStages[obj] == EntryStage.Inside;
    }

}
