using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainLoop : MonoBehaviour
{
    const int MAX_INSTRURCTION_NUMBER = 4;

    //************ SERIALIZED VARIABLES ********//
    [SerializeField] DialogueCanvas dialogueCanvas;
    [SerializeField] GameCanvas gameCanvas;
    [SerializeField] Sprite emptySprite;

    [SerializeField] ChainController chainController;
    [SerializeField] BaseProteinChain InitProteinInfo;
    [SerializeField] BaseProteinChain FinalProteinInfo;

    //************ UNITY OBJECTS ***************//
    //************ VARIABLES *******************//
    Instruction[] instructions;
    int aberrations;

    //************ PROPERTIES ******************//
    public Instruction[] Instructions
    {
        get { return instructions; }
        set { instructions = value; }
    }

    //************ UNITY INTERFACES ************// 
    // Start is called before the first frame update
    void Start()
    {
        // Init controls
        dialogueCanvas.Init(emptySprite);
        dialogueCanvas.CharacterDialogue("Metatron", "I see you but you dont see me!");

        //gameCanvas.Init();

        chainController.InitProteinInfo = InitProteinInfo;
        chainController.FinalProteinInfo = FinalProteinInfo;
        chainController.InitializeChain();

        aberrations = 0;

        // Init instructions
        instructions = new Instruction[4]{
            new NextTo(AminoAcidID.Red, AminoAcidID.Blue),
            new NextTo(AminoAcidID.Yellow, AminoAcidID.Black),
            new Instruction() , new Instruction()};

        UpdateInstructions();
        chainController.AddInstructionByIndex(0, Instructions[0]);
        chainController.AddInstructionByIndex(1, Instructions[1]);
        chainController.AddInstructionByIndex(2, Instructions[2]);
        chainController.AddInstructionByIndex(3, Instructions[3]);

    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    //************ MEMBER METHODS **************//
    private void UpdateInstructions()
    {
        // Init the AA bond location in each instruction        
        foreach (Instruction instruction in instructions)
        {
            foreach (BondInfo initBond in InitProteinInfo.ProteinChain)
            {
                instruction.EditMainPair(initBond.AminoAcidID, initBond.BondID);
                instruction.EditAssociatedPair(initBond.AminoAcidID, initBond.BondID);
            }            
        }
    }

    public void GetAberrations()
    {
        aberrations = chainController.CalculateAberrations();

        Debug.Log("Aberrations " + aberrations);
    }

    public void CharacterDialogue(string characterName, string characterDialogue)
    {
        dialogueCanvas.CharacterDialogue(characterName, characterDialogue);
    }
}
