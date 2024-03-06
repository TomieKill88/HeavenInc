using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProteinChain", menuName = "Create Protein")]
public class BaseProteinChain : ScriptableObject
{
    [SerializeField] BondInfo[] proteinChain;

    public BondInfo[] ProteinChain
    {
        get { return proteinChain; }
        set { proteinChain = value; }
    }
}

public enum AminoAcidID : ushort
{
    Empty = 0,
    Yellow,
    Red,
    Black,
    Green,
    Blue,
    Orange      
}

[System.Serializable]
public struct BondInfo
{
    public int BondID;
    public AminoAcidID AminoAcidID;
    public float AminoAcidOrientation;
    public Sprite AminoAcidSprite;
}