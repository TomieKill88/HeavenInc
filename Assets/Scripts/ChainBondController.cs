using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChainBondController : MonoBehaviour, IDropHandler
{
    //************ SERIALIZED VARIABLES ********//
    [SerializeField] int chainBondID;

    //************ UNITY OBJECTS ***************//
    AminoAcidController aminoAcidController;

    //************ VARIABLES *******************//
    //************ PROPERTIES ******************//
    public int ChainBondID
    {
        get { return chainBondID; }
        set { chainBondID = value; }
    }

    public AminoAcidController AminoAcidController
    {
        get { return aminoAcidController; }
        set { aminoAcidController = value; }
    }

    //************ UNITY INTERFACES ************// 
    void Awake()
    {
        aminoAcidController = transform.GetComponentsInChildren<AminoAcidController>(false)[0];
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        aminoAcidController = droppedObject.GetComponent<AminoAcidController>();

        // First check that slot has no children already
        if (transform.childCount == 0)
        {
            // We assign this slot as the new parent of the protein
            aminoAcidController.ParentAfterDrag = transform;
        }
        else if(transform.childCount == 1)
        {
            // We assign the child of the slot the slot of the protein being dropped 
            Transform slotChildTransform = transform.GetChild(0);
            slotChildTransform.parent = aminoAcidController.ParentAfterDrag;
            // We assign this slot as the new parent of the protein
            aminoAcidController.ParentAfterDrag = transform;
        }
    }

    //************ MEMBER METHODS **************//
}
