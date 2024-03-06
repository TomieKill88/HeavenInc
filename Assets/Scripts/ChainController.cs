using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainController : MonoBehaviour
{
    //************ SERIALIZED VARIABLES ********//
    [SerializeField] BaseProteinChain InitProteinInfo;
    [SerializeField] BaseProteinChain FinalProteinInfo;
    [SerializeField] Sprite EmptySprite;

    //************ UNITY OBJECTS ***************//
    //************ VARIABLES *******************//
    private int aberrations;

    //************ PROPERTIES ******************//
    public int Aberrations
    {
        get { return aberrations; }
        set { aberrations = value; }
    }

    //************ UNITY INTERFACES ************//
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("");

        aberrations = 0;

        InitializeChain();
    }

    //************ MEMBER METHODS **************//
    public int CalculateAberrations()
    {
        aberrations = 0;

        bool aminoAcidChecked = false;

        foreach (BondInfo finalBond in FinalProteinInfo.ProteinChain)
        {
            foreach (ChainBondController chainBond in transform.GetComponentsInChildren<ChainBondController>())
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

    private void InitializeChain()
    {
        bool aminoAcidSet = false;

        foreach (BondInfo initBond in InitProteinInfo.ProteinChain)
        {
            foreach (ChainBondController chainBond in transform.GetComponentsInChildren<ChainBondController>())
            {
                aminoAcidSet = false;

                if(initBond.BondID == chainBond.ChainBondID)
                {
                    chainBond.AminoAcidController.AminoAcidID = initBond.AminoAcidID;
                    chainBond.AminoAcidController.AminoAcidOrientation = initBond.AminoAcidOrientation;
                    chainBond.AminoAcidController.AminoAcidSprite = initBond.AminoAcidSprite;
                    chainBond.AminoAcidController.UpdateSprite();

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

        foreach (ChainBondController chainBond in transform.GetComponentsInChildren<ChainBondController>())
        {
            chainBond.AminoAcidController.AminoAcidID = 0;
            chainBond.AminoAcidController.AminoAcidOrientation = 0.0f;
            chainBond.AminoAcidController.AminoAcidSprite = EmptySprite;
            chainBond.AminoAcidController.UpdateSprite();
        }
    }
}
