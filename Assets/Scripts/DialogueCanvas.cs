using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCanvas : MonoBehaviour
{
    [SerializeField] DialogueBox dialogueBox;
    [SerializeField] DialogueBox nameBox;
    [SerializeField] Image bgImage; 

    // Start is called before the first frame update
    public void Init(Sprite bgSprite)
    {
        // INIT DIALOGUE BOXES
        nameBox.Init();
        dialogueBox.Init();
        nameBox.HideBox(false);
        dialogueBox.HideBox(false);
        BackgroundSprite(bgSprite);
    }

    public void BackgroundSprite(Sprite bgSprite)
    {
        bgImage.sprite = bgSprite; 
    }

    public void CharacterDialogue(string characterName, string characterDialogue)
    {
        nameBox.SetDialogue(characterName);
        StartCoroutine(dialogueBox.TypeDialogue(characterDialogue));
    }
}
