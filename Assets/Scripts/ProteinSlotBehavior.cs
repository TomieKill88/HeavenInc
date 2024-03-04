using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProteinSlotBehavior : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // First check that slot has no children already
        if(transform.childCount == 0)
        {
            GameObject droppedObject = eventData.pointerDrag;
            ProteinMoveControl proteinControl = droppedObject.GetComponent<ProteinMoveControl>();
            // We assign this slot as the new parent of the protein
            proteinControl.parentAfterDrag = transform;
        }
    }
}
