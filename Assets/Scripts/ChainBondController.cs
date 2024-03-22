using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void Notify(AminoAcidController aminoAcid, int newBondID);

public class ChainBondController : MonoBehaviour, IDropHandler
{
    public event Notify AminoAcidDropped;

    //************ SERIALIZED VARIABLES ********//
    [SerializeField] int chainBondID;
    [SerializeField] AminoAcidController aminoAcidController;

    //************ UNITY OBJECTS ***************//

    //************ VARIABLES *******************//
    private int chainBondColumn;
    private int chainBondRow;
    //************ PROPERTIES ******************//

    public int ChainBondID
    {
        get { return chainBondID; }
        set { chainBondID = value; }
    }

    public int ChainBondColumn
    {
        get { return chainBondColumn; }
    }

    public int ChainBondRow
    {
        get { return chainBondRow; }
    }

    public AminoAcidController AminoAcidController
    {
        get { return aminoAcidController; }
        set { aminoAcidController = value; }
    }

    //************ UNITY INTERFACES ************// 
    void Awake()
    {
        //aminoAcidController = transform.GetComponentsInChildren<AminoAcidController>(false)[0];

        chainBondRow = (int)(chainBondID / 5);
        chainBondColumn = (int)(chainBondID % 5);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;


        // First check that slot has no children already
        if (transform.childCount == 0)
        {
            // Update aminoacid
            aminoAcidController = droppedObject.GetComponent<AminoAcidController>();

            // We assign this slot as the new parent of the protein
            aminoAcidController.ParentAfterDrag = transform;
        }
        else if(transform.childCount == 1)
        {
            // Make deep copy of aminoacids before anything happens
            AminoAcidController tmpBondAminoacid = aminoAcidController.DeepCopy();
            AminoAcidController tmpDroppedAminoacid = droppedObject.GetComponent<AminoAcidController>().DeepCopy();

            // Drop functionality
            // Update aminoacid
            /*aminoAcidController = droppedObject.GetComponent<AminoAcidController>();

            // We take the current child AminoAcid in the Bond and Change the Bond for the one of the AminoAcid being dropped 
            Transform slotChildTransform = transform.GetChild(0);
            slotChildTransform.SetParent(aminoAcidController.ParentAfterDrag, false);//.parent = aminoAcidController.ParentAfterDrag;

            // We assign this slot as the new parent of the protein
            aminoAcidController.ParentAfterDrag = transform;
            */

            // Inform the Chain controller that an AminoAcid has changed bonds
            // Current AA now goes to the bond of dropped AA
            OnAminoAcidDropped(tmpBondAminoacid, tmpDroppedAminoacid.CurrentBondID);
            // Dropped AA now has this bond
            OnAminoAcidDropped(tmpDroppedAminoacid, tmpBondAminoacid.CurrentBondID);
        }
    }

    //************ MEMBER METHODS **************//   

    protected virtual void OnAminoAcidDropped(AminoAcidController aminoAcid, int newBondID)
    {
        AminoAcidDropped?.Invoke(aminoAcid, newBondID);
    }
}
