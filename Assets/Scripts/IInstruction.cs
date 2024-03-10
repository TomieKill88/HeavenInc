using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction
{
    //************ SERIALIZED VARIABLES ********//
    //************ UNITY OBJECTS ***************//
    //************ VARIABLES *******************//
    protected string description;
    protected Dictionary <AminoAcidID, int> mainAminoAcids;
    protected Dictionary<AminoAcidID, int> associatedAminoAcids;

    //************ PROPERTIES ******************//
    public string Description
    {
        get { return description; }
    }

    public Dictionary<AminoAcidID, int> MainAminoAcids
    {
        get { return mainAminoAcids; }
        set { mainAminoAcids = value; }
    }

    public Dictionary<AminoAcidID, int> AssociatedAminoAcids
    {
        get { return associatedAminoAcids; }
        set { associatedAminoAcids = value; }
    }


    //************ MEMBER METHODS **************//
    public Instruction()
    {
        description = "Empty instruction. Always False";

        mainAminoAcids = new Dictionary<AminoAcidID, int>();
        associatedAminoAcids = new Dictionary<AminoAcidID, int>();

        mainAminoAcids[AminoAcidID.Empty] = 0;
        associatedAminoAcids[AminoAcidID.Empty] = 0;
    }

    public Instruction(AminoAcidID pMainAminoAcid, AminoAcidID pAssociatedAminoAcids)
    {
        description = "Empty instruction. Always False";

        mainAminoAcids = new Dictionary<AminoAcidID, int>();
        associatedAminoAcids = new Dictionary<AminoAcidID, int>();

        mainAminoAcids[pMainAminoAcid] = 0;
        associatedAminoAcids[pAssociatedAminoAcids] = 0;
    }

    public void AddMainPair(AminoAcidID aaID, int cbControllerID)
    {
        if (!mainAminoAcids.ContainsKey(aaID))
        {
            mainAminoAcids[aaID] = cbControllerID;
        }
    }

    public bool EditMainPair(AminoAcidID aaID, int cbControllerID)
    {
        if (mainAminoAcids.ContainsKey(aaID))
        {
            mainAminoAcids[aaID] = cbControllerID;
            return true;
        }

        return false;
    }

    public void ClearMainPair()
    {
        mainAminoAcids.Clear();
    }

    public void AddAssociatedPair(AminoAcidID aaID, int cbControllerID)
    {
        if (!associatedAminoAcids.ContainsKey(aaID))
        {
            associatedAminoAcids[aaID] = cbControllerID;
        }
    }

    public bool EditAssociatedPair(AminoAcidID aaID, int cbControllerID)
    {
        if (associatedAminoAcids.ContainsKey(aaID))
        {
            associatedAminoAcids[aaID] = cbControllerID;
            return true;
        }
        return false;
    }

    public void ClearAssociatedPair()
    {
        associatedAminoAcids.Clear();
    }

    public int GetBondRow(int chainBondID)
    {
        // 5 is the max number of columns
        return (int)(chainBondID / 5);
    }

    public int GetBondColumn(int chainBondID)
    {
        // 5 is the max number of columns
        return (int)(chainBondID % 5);
    }

    //************ VIRTUAL MEMBER METHODS **************//
    public virtual bool Apply()
    {
        return true;
    }
}
