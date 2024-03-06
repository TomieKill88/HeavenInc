using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProteinSlotBehavior : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        ProteinMoveControl proteinControl = droppedObject.GetComponent<ProteinMoveControl>();        

        // First check that slot has no children already
        if (transform.childCount == 0)
        {
            // We assign this slot as the new parent of the protein
            proteinControl.ParentAfterDrag = transform;
        }
        else if(transform.childCount == 1)
        {
            // We assign the child of the slot the slot of the protein being dropped 
            Transform slotChildTransform = transform.GetChild(0);
            slotChildTransform.parent = proteinControl.ParentAfterDrag;
            // We assign this slot as the new parent of the protein
            proteinControl.ParentAfterDrag = transform;
        }
    }
}
