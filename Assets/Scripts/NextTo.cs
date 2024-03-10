using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTo : Instruction
{
    //************ SERIALIZED VARIABLES ********//
    //************ UNITY OBJECTS ***************//
    //************ VARIABLES *******************//
    //************ PROPERTIES ******************//
    //************ MEMBER METHODS **************//
    public NextTo()
    {
        description = "Empty instruction. Always True";

        mainAminoAcids = new Dictionary<AminoAcidID, int>();
        associatedAminoAcids = new Dictionary<AminoAcidID, int>();

        mainAminoAcids[AminoAcidID.Empty] = 0;
        associatedAminoAcids[AminoAcidID.Empty] = 0;
    }

    public NextTo(AminoAcidID pMainAminoAcid, AminoAcidID pAssociatedAminoAcids) : 
        base(pMainAminoAcid, pAssociatedAminoAcids)
    {
        description = "AminoAcid " + pMainAminoAcid.ToString() + " MUST be next to " + pAssociatedAminoAcids.ToString();
    }

    //************ OVERIDED MEMBER METHODS **************//
    public override bool Apply()
    {
        Debug.Log("APPLY");
        foreach (KeyValuePair<AminoAcidID, int> mBond in MainAminoAcids)
        {
            Debug.Log(mBond.Key.ToString() + " in " + GetBondColumn(mBond.Value) + ", " + GetBondRow(mBond.Value));
            foreach (KeyValuePair<AminoAcidID, int> aBond in AssociatedAminoAcids)
            {
                Debug.Log(aBond.Key.ToString() + " in " + GetBondColumn(aBond.Value) + ", " + GetBondRow(aBond.Value));
                if (GetBondColumn(aBond.Value) == GetBondColumn(mBond.Value) + 1 || GetBondColumn(aBond.Value) == GetBondColumn(mBond.Value) - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }    
}
