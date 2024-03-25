using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainController : MonoBehaviour
{
    const int MAX_INSTRUCTION_NUMBER = 4;

    //************ SERIALIZED VARIABLES ********//
    [SerializeField] Sprite EmptySprite;
    [SerializeField] ChainBondController[] ChainBondControllers;

    //************ UNITY OBJECTS ***************//
    //************ VARIABLES *******************//
    private int aberrations;
    Instruction[] instructions;

    //************ PROPERTIES ******************//
    public int Aberrations
    {
        get { return aberrations; }
        set { aberrations = value; }
    }

    public Instruction[] Instructions
    {
        get { return instructions; }
        set { instructions = value; }
    }

    public BaseProteinChain InitProteinInfo { get; set; }
    public BaseProteinChain FinalProteinInfo { get; set; }

    //************ UNITY INTERFACES ************//
    // Start is called before the first frame update
    void Awake()
    {
        aberrations = 0;

        // At the moment we will have only 4 instructions MAX
        instructions = new Instruction[MAX_INSTRUCTION_NUMBER]{
            new Instruction() , new Instruction(),
            new Instruction() , new Instruction()};
    }

    //************ MEMBER METHODS **************//
    public int CalculateAberrations()
    {
        aberrations = 0;

        // Get bonds
        foreach (Instruction instruction in instructions)
        {
            if (!instruction.Apply())
                aberrations++;
        }

        return aberrations;
    }

    public int CalculateAberrationsFixed()
    {
        aberrations = 0;

        bool aminoAcidChecked = false;

        // transform.GetComponentsInChildren<ChainBondController>()
        foreach (BondInfo finalBond in FinalProteinInfo.ProteinChain)
        {
            foreach (ChainBondController chainBond in ChainBondControllers)
            {
                aminoAcidChecked = false;

                if (finalBond.BondID == chainBond.ChainBondID)
                {
                    if (!(chainBond.AminoAcidController.AminoAcidID == finalBond.AminoAcidID &&
                          chainBond.AminoAcidController.AminoAcidOrientation == finalBond.AminoAcidOrientation))
                    {
                        aberrations++;
                    }

                    aminoAcidChecked = true;
                    break;
                }

                if (aminoAcidChecked)
                    break;
            }
        }

        return aberrations;
    }

    public void InitializeChain()
    {
        bool aminoAcidSet = false;

        foreach (ChainBondController chainBond in ChainBondControllers)
        {
            // Set Event calls for each bond
            chainBond.AminoAcidDropped += ChainBondAminoAcidDropped;
            //Set the aminocid bonid
            chainBond.AminoAcidController.CurrentBondID = chainBond.ChainBondID;
        }

        foreach (BondInfo initBond in InitProteinInfo.ProteinChain)
        {
            foreach (ChainBondController chainBond in ChainBondControllers)
            {
                aminoAcidSet = false;

                if (initBond.BondID == chainBond.ChainBondID)
                {
                    chainBond.AminoAcidController.AminoAcidID = initBond.AminoAcidID;
                    chainBond.AminoAcidController.AminoAcidOrientation = initBond.AminoAcidOrientation;
                    chainBond.AminoAcidController.AminoAcidSprite = initBond.AminoAcidSprite;
                    chainBond.AminoAcidController.UpdateSprite(true);

                    aminoAcidSet = true;
                    break;
                }

                if(aminoAcidSet)
                    break;
            }
        }            
    }

    private void EraseChain()
    {
        aberrations = 0;

        foreach (ChainBondController chainBond in ChainBondControllers)
        {
            chainBond.AminoAcidController.AminoAcidID = 0;
            chainBond.AminoAcidController.AminoAcidOrientation = 0.0f;
            chainBond.AminoAcidController.AminoAcidSprite = EmptySprite;
            chainBond.AminoAcidController.UpdateSprite(false);
        }
    }

    public void AddInstructionByIndex(int index, Instruction instruction)
    {
        if (!(index + 1 >= MAX_INSTRUCTION_NUMBER))
        {
            instructions[index] = instruction;
        }
    }

    public void ChainBondAminoAcidDropped(AminoAcidController aminoAcid, int newBondID)
    {
        ChainBondControllers[newBondID].AminoAcidController.CurrentBondID = newBondID;
        ChainBondControllers[newBondID].AminoAcidController.UpdateAminoAcid(aminoAcid);

        // Update bonds
        foreach (Instruction instruction in instructions)
        {
            instruction?.EditMainPair(aminoAcid.AminoAcidID, newBondID);
            instruction?.EditAssociatedPair(aminoAcid.AminoAcidID, newBondID);
        }        
    }

    public void GetBondsInfo()
    {
        foreach(ChainBondController chainBond in ChainBondControllers)
        {
            if(chainBond.AminoAcidController.AminoAcidID != AminoAcidID.Empty)
                Debug.Log(chainBond.AminoAcidController.AminoacidToString());
        }
    }
}
